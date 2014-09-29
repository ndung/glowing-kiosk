#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include "ssp_helpers.h"

#ifdef WIN32
#include "port_win32.h"
#else
#include "port_linux.h"
#include "../inc/SSPComs.h"
#include <unistd.h>
#endif

#include<sys/socket.h>
#include<arpa/inet.h> //inet_addr
#include<pthread.h> //for threading , link with lpthread

//the thread function
void *connection_handler(void *);
void parse_poll(SSP_COMMAND *sspC, SSP_POLL_DATA6 *poll);
void error(char *msg)
{
    perror(msg);
    //exit(1);
}

int sock;
char trx_id[7];
char cmd_code[3];
char dev_code[3];
char payout_data[255];
char denom_amount[13];
char float_amount[13];
char credit_amount[1];
char payout_amount[13];

char *cc;
char *port_c;

int running=0;
int floating=0;
int dispensing=0;

SSP_COMMAND *sspC;

// pase the validators response to the poll command. the SSP_POLL_DATA6 structure has an
// array of structures which contain values and country codes
void parse_poll(SSP_COMMAND *sspC, SSP_POLL_DATA6 * poll)
{
    int i;    
    for (i = 0; i < poll->event_count; ++i)
    {
        switch(poll->events[i].event)
        {
        case SSP_POLL_RESET:
            printf("Unit Reset\n");
            // Make sure we are using ssp version 6
            if (ssp6_host_protocol(sspC, 0x06) != SSP_RESPONSE_OK)
            {
                printf("Host Protocol Failed\n");
                return;
            }
            else{
                generate_success_response("05");
            }
            break;
        case SSP_POLL_READ:
            // the 'read' event contains 1 data value, which if >0 means a note has been validated and is in escrow
            if (poll->events[i].data1 > 0){
                printf("Note Read %ld %s\n", poll->events[i].data1, poll->events[i].cc);
            }
            break;
        case SSP_POLL_CREDIT:
            // The note which was in escrow has been accepted
            printf("Credit %ld %s\n", poll->events[i].data1, poll->events[i].cc);
            sprintf(credit_amount,"%d", poll->events[i].data1);
            break;
        case SSP_POLL_INCOMPLETE_PAYOUT:
            // the validator shutdown during a payout, this event is reporting that some value remains to payout
            printf("Incomplete payout %ld of %ld %s\n", poll->events[i].data1, poll->events[i].data2, poll->events[i].cc);
            break;
        case SSP_POLL_INCOMPLETE_FLOAT:
            // the validator shutdown during a float, this event is reporting that some value remains to float
            printf("Incomplete float %ld of %ld %s\n", poll->events[i].data1, poll->events[i].data2, poll->events[i].cc);
            break;
        case SSP_POLL_REJECTING:
            break;
        case SSP_POLL_REJECTED:
            // The note was rejected
            printf("Note Rejected\n");
            break;
        case SSP_POLL_STACKING:
            printf("Stacking\n");
            break;
        case SSP_POLL_STORED:
            // The note has been stored in the payout unit
            generate_note_info_data("000000000000");
            printf("Stored\n");
            break;
        case SSP_POLL_STACKED:
            // The note has been stacked in the cashbox            
            generate_note_info_data("100000000000");
            printf("Stacked\n");
            break;
        case SSP_POLL_SAFE_JAM:
            printf("Safe Jam\n");
            break;
        case SSP_POLL_UNSAFE_JAM:
            printf("Unsafe Jam\n");
            break;
        case SSP_POLL_JAMMED:
            printf("Unit Jammed\n");
            break;
        case SSP_POLL_HALTED:
            printf("Halted\n");
            break;
        case SSP_POLL_TIMEOUT:
            printf("Timeout searching for a note\n");
            break;
        case SSP_POLL_DISABLED:
            // The validator has been disabled
            printf("DISABLED\n");
            break;
        case SSP_POLL_FRAUD_ATTEMPT:
            // The validator has detected a fraud attempt
            printf("Fraud Attempt %ld %s\n", poll->events[i].data1, poll->events[i].cc);
            break;
        case SSP_POLL_STACKER_FULL:
            // The cashbox is full
            printf("Stacker Full\n");
            break;
        case SSP_POLL_CASH_BOX_REMOVED:
            // The cashbox has been removed
            printf("Cashbox Removed\n");
            break;
        case SSP_POLL_CASH_BOX_REPLACED:
            // The cashbox has been replaced
            printf("Cashbox Replaced\n");
            break;
        case SSP_POLL_CLEARED_FROM_FRONT:
            // A note was in the notepath at startup and has been cleared from the front of the validator
            printf("Cleared from front\n");
            break;
        case SSP_POLL_CLEARED_INTO_CASHBOX:
            // A note was in the notepath at startup and has been cleared into the cashbox
            printf("Cleared Into Cashbox\n");
            break;
        case SSP_POLL_CALIBRATION_FAIL:
            // the hopper calibration has failed. An extra byte is available with an error code.
            printf("Calibration fail: ");
            
            switch(poll->events[i].data1) {
            case NO_FAILUE:
                printf ("No failure\n");
            case SENSOR_FLAP:
                printf ("Optical sensor flap\n");
            case SENSOR_EXIT:
                printf ("Optical sensor exit\n");
            case SENSOR_COIL1:
                printf ("Coil sensor 1\n");
            case SENSOR_COIL2:
                printf ("Coil sensor 2\n");
            case NOT_INITIALISED:
                printf ("Unit not initialised\n");
            case CHECKSUM_ERROR:
                printf ("Data checksum error\n");
            case COMMAND_RECAL:
                printf ("Recalibration by command required\n");
                ssp6_run_calibration(sspC);
            }
            break;
        case SSP_POLL_DISPENSING:
            // A note was in the notepath at startup and has been cleared into the cashbox
            dispensing = 1;
            printf("Dispensing\n");
            break;
        case SSP_POLL_DISPENSED:
            dispensing = 0;
            // A note was in the notepath at startup and has been cleared into the cashbox
            printf("Dispensed\n");                        
            generate_success_response("03");
            break;
        case SSP_POLL_SMART_EMPTYING:
            // A note was in the notepath at startup and has been cleared into the cashbox
            printf("Smart emptying\n");
            break;
        case SSP_POLL_SMART_EMPTIED:
            // A note was in the notepath at startup and has been cleared into the cashbox
            printf("Smart emptied\n");                        
            generate_success_response("06");
            break;	
        case SSP_POLL_FLOATING:
            floating = 1;
            printf("Floating\n");
            break;
        case SSP_POLL_FLOATED:
            floating = 0;
            printf("Floated\n");            
            generate_success_response("07");
            break;
        }	
    }
}

void run_validator(SSP_COMMAND *ssp)
{
    sspC = ssp;
    SSP_POLL_DATA6 poll;       
    SSP_RESPONSE_ENUM ssp_response;

    //check validator is present
    ssp_response = ssp6_sync(sspC);
    if (ssp_response != SSP_RESPONSE_OK)
    {
        printf("NO VALIDATOR FOUND\n");
        generate_response(ssp_response);
        return;
    }
    printf ("Validator Found\n");
    
    //try to setup encryption using the default key
    ssp_response = ssp6_setup_encryption(sspC,(unsigned long long)0x123456701234567LL);
    if (ssp_response != SSP_RESPONSE_OK){
        printf("Encryption Failed\n");
        generate_response(ssp_response);
        return;
    }    
    printf("Encryption Setup\n");    
    
    // Make sure we are using ssp version 6
    ssp_response = ssp6_host_protocol(sspC, 0x06);
    if (ssp_response != SSP_RESPONSE_OK)
    {
        printf("Host Protocol Failed\n");
        generate_response(ssp_response);
        return;
    }
    
    unsigned int i=0;
    SSP6_SETUP_REQUEST_DATA setup_req;
    // Collect some information about the validator
    ssp_response = ssp6_setup_request(sspC, &setup_req);
    if (ssp_response != SSP_RESPONSE_OK) {
        generate_response(ssp_response);
        printf("Setup Request Failed\n");
        return;
    }
    printf("Firmware: %s\n", setup_req.FirmwareVersion);
    printf("Channels:\n");
    for (i=0; i<setup_req.NumberOfChannels; i++){
        printf("channel %d: %d %s\n", i+1, setup_req.ChannelData[i].value, setup_req.ChannelData[i].cc);
        cc = setup_req.ChannelData[i].cc;
        route_note_to_storage(setup_req.ChannelData[i].value);	
    }
    
    //enable the validator
    ssp_response = ssp6_enable(sspC);
    if (ssp_response != SSP_RESPONSE_OK)
    {
        printf("Enable Failed\n");
        generate_response(ssp_response);
        return;
    }
    
    if (setup_req.UnitType == 0x03) {
        // SMART Hopper requires different inhibit commands
        for (i=0; i<setup_req.NumberOfChannels; i++){
            ssp6_set_coinmech_inhibits(sspC, setup_req.ChannelData[i].value, setup_req.ChannelData[i].cc, ENABLED);
        }
    } else {
        if (setup_req.UnitType == 0x06 || setup_req.UnitType == 0x07) {
            //enable the payout unit
            ssp_response = ssp6_enable_payout(sspC, setup_req.UnitType);
            if (ssp_response != SSP_RESPONSE_OK)
            {
                printf("Enable Failed\n");
                generate_response(ssp_response);
                return;
            }
        }
        
        // set the inhibits (enable all note acceptance)
        ssp_response = ssp6_set_inhibits(sspC,0xFF,0xFF);
        if (ssp_response != SSP_RESPONSE_OK)
        {
            printf("Inhibits Failed\n");
            generate_response(ssp_response);
            return;
        }
    }
    
    generate_response(ssp_response);
    running=1;
    
    while(running==1)
    {
        //poll the unit
        SSP_RESPONSE_ENUM rsp_status;
        if ((rsp_status = ssp6_poll(sspC, &poll)) != SSP_RESPONSE_OK)
        {
            if (rsp_status == SSP_RESPONSE_TIMEOUT) {
                // If the poll timed out, then give up
                printf("SSP Poll Timeout\n");
                generate_response(rsp_status);
                running = 0;
                return;
            } else {
                if (rsp_status == 0xFA) {
                    // The validator has responded with key not set, so we should try to negotiate one
                    if (ssp6_setup_encryption(sspC,(unsigned long long)0x123456701234567LL) != SSP_RESPONSE_OK)
                        printf("Encryption Failed\n");
                    else
                        printf("Encryption Setup\n");
                } else {
                    printf ("SSP Poll Error: 0x%x\n", rsp_status);
                }
            }                        
        }
        parse_poll(sspC, &poll);  
        do_action();       
        usleep(500000); //500 ms delay between polls
    }
}

void enable(){
    unsigned int i=0;
    SSP6_SETUP_REQUEST_DATA setup_req;
    SSP_RESPONSE_ENUM ssp_response;
    // Collect some information about the validator
    ssp_response = ssp6_setup_request(sspC, &setup_req);
    if (ssp_response != SSP_RESPONSE_OK) {
        printf("Setup Request Failed\n");
        generate_response(ssp_response);
        return;
    }
    printf("Firmware: %s\n", setup_req.FirmwareVersion);
    printf("Channels:\n");
    for (i=0; i<setup_req.NumberOfChannels; i++){
        printf("channel %d: %d %s\n", i+1, setup_req.ChannelData[i].value, setup_req.ChannelData[i].cc);        
    }
    
    //enable the validator
    ssp_response = ssp6_enable(sspC);
    if (ssp_response != SSP_RESPONSE_OK)
    {
        printf("Enable Failed\n");
        generate_response(ssp_response);
        return;
    }
    
    if (setup_req.UnitType == 0x03) {
        // SMART Hopper requires different inhibit commands
        for (i=0; i<setup_req.NumberOfChannels; i++){
            ssp6_set_coinmech_inhibits(sspC, setup_req.ChannelData[i].value, setup_req.ChannelData[i].cc, ENABLED);
        }
    } else {
        if (setup_req.UnitType == 0x06 || setup_req.UnitType == 0x07) {
            //enable the payout unit
            ssp_response = ssp6_enable_payout(sspC, setup_req.UnitType);
            if (ssp_response != SSP_RESPONSE_OK)
            {
                printf("Enable Failed\n");
                generate_response(ssp_response);
                return;
            }
        }
        
        // set the inhibits (enable all note acceptance)
        ssp_response = ssp6_set_inhibits(sspC,0xFF,0xFF);
        if (ssp_response != SSP_RESPONSE_OK)
        {
            printf("Inhibits Failed\n");
            generate_response(ssp_response);
            return;
        }
    }
    
    generate_response(ssp_response);
}

void do_action(){
    SSP_RESPONSE_ENUM ssp_response;
    if (strcmp(cmd_code,"03")==0){
        printf("do action for dispensing\n");
        if (dispensing==0){
            int amount = (int)(strtod(payout_amount, NULL)*100);
            memset(payout_amount, 0, 13);
            make_payout(amount);
        }
    }
    else if (strcmp(cmd_code,"05")==0){
        printf("do action for reseting\n");
        ssp_response = ssp6_reset(sspC);
        if (ssp_response != SSP_RESPONSE_OK){
            printf("ERROR: Reset failed\n");
            generate_response(ssp_response);
        }else{
            printf("Reset succeed\n");
        }
    }	
    else if (strcmp(cmd_code,"06")==0){
        printf("do action for emptying\n");
        ssp_response = ssp6_smart_empty(sspC);
        if (ssp_response != SSP_RESPONSE_OK){
            printf("ERROR: Empty failed\n");
            generate_response(ssp_response);
        }else{
            printf("Empty succeed\n");
        }
    }		
    else if (strcmp(cmd_code,"07")==0){
        printf("do action for floating\n");
        if (floating==0){
            int denom = (int)(strtod(denom_amount, NULL)*100);
            int min_value = (int)(strtod(float_amount, NULL)*100);
            memset(denom_amount, 0, 13);
            memset(float_amount, 0, 13);
            make_float(min_value,denom);
        }
    }
    else if (strcmp(cmd_code,"08")==0){
        printf("do action for routing to storage\n");
        int pay = (int)(strtod(denom_amount, NULL));
        long amount = pay*100;
        SSP_RESPONSE_ENUM ssp_response = ssp6_set_route(sspC, amount, cc, 0x00);
        generate_response(ssp_response);
        memset(denom_amount, 0, 13);
    }		
    else if (strcmp(cmd_code,"09")==0){
        printf("do action for routing to cashbox\n");
        int pay = (int)(strtod(denom_amount, NULL));
        long amount = pay*100;
        SSP_RESPONSE_ENUM ssp_response = ssp6_set_route(sspC, amount, cc, 0x01);
        generate_response(ssp_response);
        memset(denom_amount, 0, 13);
    }		    
    else if (strcmp(cmd_code,"10")==0){
        printf("do action for checking note level\n");
        int amount = (int)(strtod(denom_amount, NULL)*100);
        SSP_RESPONSE_ENUM ssp_response = ssp6_check_note_level(sspC, amount, cc);
        if (ssp_response != SSP_RESPONSE_OK){
            printf("ERROR: Check note level failed\n");
            generate_response(ssp_response);
        }else{
            int level =(unsigned int)sspC->ResponseData[1];
            char level_str[13];
            sprintf(level_str, "%012d", level);
            level_str[12] = '\0';

            char rs_data[25];
            strcpy (rs_data, denom_amount);
            strcat (rs_data, level_str);
            rs_data[24]='\0';
            generate_success_response_data(rs_data, 24);
        }
        memset(denom_amount, 0, 13);
    }        
    else if (strcmp(cmd_code,"11")==0){
        printf("do action for enabling\n");
        enable();
    }		
    else if (strcmp(cmd_code,"12")==0){
        printf("do action for disabling validator\n");
        ssp_response = ssp6_disable(sspC);
        generate_response(ssp_response);
    }
    else if (strcmp(cmd_code,"13")==0){
        printf("do action for disabling payout\n");
        ssp_response = ssp6_disable_payout(sspC);
        generate_response(ssp_response);
    }
    else if (strcmp(cmd_code,"14")==0){
        printf("do action for getting cashbox payout op data\n");
        SSP_RESPONSE_ENUM ssp_response = ssp6_get_cashbox_payout_op_data(sspC);
        if (ssp_response != SSP_RESPONSE_OK){
            printf("ERROR: Get cashbox payout op data failed\n");
            generate_response(ssp_response);
        }else{
            printf("get cashbox payout op data succeed\n");
            int num = sspC->ResponseData[1] << 0;
            char num_str[2];
            sprintf(num_str,"%d", num);
            num_str[1] = '\0';
            char rs_data[num*24+14];
            strcpy (rs_data,num_str);
            int i = 0;
            for (i = 2; i < (9 * num); i += 9)
            {
                int level = sspC->ResponseData[i] | sspC->ResponseData[i+1] << 8;
                char level_str[13];
                sprintf(level_str, "%012d", level);
                level_str[12] = '\0';

                int denom = sspC->ResponseData[i+2] | sspC->ResponseData[i+3] << 8 | sspC->ResponseData[i+4] << 16 | sspC->ResponseData[i+5] << 24;
                char denom_str[13];
                sprintf(denom_str, "%012d", denom);
                denom_str[12] = '\0';

                strcat (rs_data, level_str);
                strcat (rs_data, denom_str);
            }
            int notes = sspC->ResponseData[i+2] | sspC->ResponseData[i+3] << 8 | sspC->ResponseData[i+4] << 16 | sspC->ResponseData[i+5] << 24;
            char notes_str[13];
            sprintf(notes_str, "%012d", notes);
            notes_str[12] = '\0';
            strcat (rs_data, notes_str);
            rs_data[num*24+13] = '\0';

            generate_success_response_data(rs_data, num*24+13);
        }
    }
    else if (strcmp(cmd_code,"15")==0){
        printf("do action for dispensing by denom\n");
        printf("payout_data: %s\n",payout_data);
        if (dispensing==0){
            SSP_RESPONSE_ENUM ssp_response = ssp6_payout_by_denom(sspC, payout_data, cc, SSP6_OPTION_BYTE_DO);
            memset(payout_data, 0, 255);
            if (ssp_response != SSP_RESPONSE_OK){
                generate_response(ssp_response);
            }
        }
    }
    memset(cmd_code, 0, 3);
}

void start()
{
    int ssp_address;
    SSP_COMMAND ssp;
    init_lib();
    
    //set the ssp address
    ssp_address = 0;
    printf("SSP ADDRESS: %d\n",ssp_address);
    
    // Setup the SSP_COMMAND structure for the validator at ssp_address
    ssp.Timeout = 1000;
    ssp.EncryptionStatus = NO_ENCRYPTION;
    ssp.RetryLevel = 3;
    ssp.BaudRate = 9600;
    
    //open the com port
    printf("PORT: %s\n", port_c);
    
    printf ("##%s##\n", port_c);
    if (open_ssp_port(port_c) == 0)
    {
        printf("Port Error\n");
        return 1;
    }
    
    //run the validator
    run_validator(&ssp);
    
    // close the com port
    close_ssp_port();
}

int main(int argc, char *argv[])
{
    port_c = argv[1];
    printf("port_c : %s\n", port_c);
    
    int socket_desc , client_sock , c;
    struct sockaddr_in server , client;
    
    //Create socket
    socket_desc = socket(AF_INET , SOCK_STREAM , 0);
    if (socket_desc == -1)
    {
        printf("Could not create socket");
    }
    puts("Socket created");
    
    //Prepare the sockaddr_in structure
    server.sin_family = AF_INET;
    server.sin_addr.s_addr = INADDR_ANY;
    server.sin_port = htons( 11000 );
    
    //Bind
    if( bind(socket_desc,(struct sockaddr *)&server , sizeof(server)) < 0)
    {
        //print the error message
        perror("bind failed. Error");
        return 1;
    }
    puts("bind done");
    
    //Listen
    listen(socket_desc , 3);
    
    //Accept and incoming connection
    puts("Waiting for incoming connections...");
    c = sizeof(struct sockaddr_in);
    pthread_t thread_id;
    
    while( (client_sock = accept(socket_desc, (struct sockaddr *)&client, (socklen_t*)&c)) )
    {
        puts("Connection accepted");
        
        if( pthread_create( &thread_id , NULL ,  connection_handler , (void*) &client_sock) < 0)
        {
            perror("could not create thread");
            return 1;
        }
        
        //Now join the thread , so that we dont terminate before the thread
        //pthread_join( thread_id , NULL);
        puts("Handler assigned");
    }
    
    if (client_sock < 0)
    {
        perror("accept failed");
        return 1;
    }
    
    return 0;
}

void *connection_handler(void *socket_desc)
{
    //Get the socket descriptor
    sock = *(int*)socket_desc;
    int read_size;
    char message[255];
    
    //Receive a message from client
    while( (read_size = recv(sock , message , 255 , 0)) > 0 )
    {
        //end of string marker
        message[read_size] = '\0';
        
        printf("message received: %s\n",message);
        
        strncpy(dev_code,&message[6],2);
        dev_code[2] = '\0';
        printf("device code: %s\n",dev_code);
        
        strncpy(cmd_code,&message[8],2);
        cmd_code[2] = '\0';
        printf("cmd code: %s\n",cmd_code);
        
        strncpy(trx_id,&message[12],6);
        trx_id[6] = '\0';
        printf("trx id: %s\n",trx_id);
        
        if (strcmp(cmd_code,"01")==0){
            if (running==0){
                printf("Start validator\n");  
                pthread_t sp_thread;
                pthread_create( &sp_thread , NULL ,  start , NULL);
            }else{
                generate_response(SSP_RESPONSE_OK);
            }
        }
        else if (strcmp(cmd_code,"03")==0){
            strncpy(payout_amount,&message[18],12);
            payout_amount[13] = 0;
            printf("payout amount: %s\n",payout_amount);
        }	
        else if (strcmp(cmd_code,"04")==0){
            running=0;
            printf("Stop validator\n");
            generate_response(SSP_RESPONSE_OK);
        }
        else if (strcmp(cmd_code,"07")==0){
            strncpy(denom_amount,&message[18],12);
            denom_amount[13] = '\0';
            strncpy(float_amount,&message[30],12);
            float_amount[13] = '\0';
        }    
        else if (strcmp(cmd_code,"08")==0){
            strncpy(denom_amount,&message[18],12);
            denom_amount[13] = '\0';
        }        
        else if (strcmp(cmd_code,"09")==0){
            strncpy(denom_amount,&message[18],12);
            denom_amount[13] = '\0';
        }        
        else if (strcmp(cmd_code,"10")==0){
            strncpy(denom_amount,&message[18],12);
            denom_amount[13] = '\0';
        }
        else if (strcmp(cmd_code,"15")==0){
            char denoms_to_payout[2];
            strncpy(denoms_to_payout,&message[18],1);
            denoms_to_payout[1] = '\0';
            int num = (int)(strtod(denoms_to_payout, NULL));
            strncpy(payout_data,&message[18],num*24+1);
            payout_data[num*24+2] = '\0';
        }
        memset(message, 0, 255);
    }
    
    if(read_size == 0)
    {
        puts("Client disconnected");
        fflush(stdout);
    }
    else if(read_size == -1)
    {
        perror("recv failed");
    }
    
    return 0;
} 


void route_note_to_storage(int pay)
{	
    long amount = pay*100;
    if (ssp6_set_route(sspC, amount, cc, 0x00) != SSP_RESPONSE_OK){
        printf("ERROR: Route to storage failed\n");
    }else{
        printf("Route to storage succeed\n");
    }	
}

void make_payout(int amount){
    SSP_RESPONSE_ENUM ssp_response = ssp6_payout(sspC, amount, cc, SSP6_OPTION_BYTE_DO);
    if (ssp_response != SSP_RESPONSE_OK){
        printf("ERROR: Payout failed");
        // when the payout fails it should return 0xf5 0xNN, where 0xNN is an error code        
        switch(sspC->ResponseData[1]) {
        case 0x01:
            printf(": Not enough value in Smart Payout\n");
            generate_response(ssp_response, "X1");
            break;
        case 0x02:
            printf(": Cant pay exact amount\n");
            generate_response(ssp_response, "X2");
            break;
        case 0x03:
            printf(": Smart Payout Busy\n");
            generate_response(ssp_response, "X3");
            break;
        case 0x04:
            printf(": Smart Payout Disabled\n");
            generate_response(ssp_response, "X4");
            break;
        default:
            printf("\n");
            generate_response(ssp_response, "X5");
        }        
    }
}

void make_float(int min, int amount){
    SSP_RESPONSE_ENUM ssp_response = ssp6_float(sspC, min, amount, cc, SSP6_OPTION_BYTE_DO);
    if (ssp_response != SSP_RESPONSE_OK){
        printf("ERROR: Float failed\n");
        generate_response(ssp_response);
    }
}

void generate_status(char* status){

}

void generate_note_info_data(char* request_data){
    char data[32];
    strcpy (data, "0027RQ");
    strcat (data, dev_code);
    strcat (data, "02");
    strcat (data, "00");
    strcat (data, trx_id);
    strcat (data, request_data);
    strcat (data, credit_amount);
    data[31]='\0';
    write(sock,data,31);
}

void generate_success_response(char* cmd){
    char response[19];
    strcpy (response, "0014RS");
    strcat (response, dev_code);
    strcat (response, cmd);
    strcat (response, "00");
    strcat (response, trx_id);
    response[18]='\0';
    write(sock,response,18);
}

void generate_success_response_data(char* response_data, int response_data_length){
    char response[19+response_data_length];
    char message_length[5];
    sprintf(message_length, "%04d", (response_data_length+14));
    message_length[4]='\0';
    strcpy (response, message_length);
    strcat (response, "RS");
    strcat (response, dev_code);
    strcat (response, cmd_code);
    strcat (response, "00");
    strcat (response, trx_id);
    strcat (response, response_data);
    response[18+response_data_length]='\0';
    printf("response: %s\n", response);
    write(sock,response,18+response_data_length);
}

void generate_response(SSP_RESPONSE_ENUM ssp_response, char* response_code){
    if (ssp_response == SSP_RESPONSE_OK){
        response_code = "00";
    }else{
        if (response_code==NULL){
            response_code = "99";
            if (ssp_response == SSP_RESPONSE_COMMAND_NOT_PROCESSED){
                response_code = "01";
            }
            else if (ssp_response == SSP_RESPONSE_FAILURE){
                response_code = "02";
            }
            else if (ssp_response == SSP_RESPONSE_KEY_NOT_SET){
                response_code = "03";
            }
            else if (ssp_response == SSP_RESPONSE_INVALID_PARAMETER){
                response_code = "04";
            }
            else if (ssp_response == SSP_RESPONSE_SOFTWARE_ERROR){
                response_code = "05";
            }
            else if (ssp_response == SSP_RESPONSE_UNKNOWN_COMMAND){
                response_code = "06";
            }
            else if (ssp_response == SSP_RESPONSE_INCORRECT_PARAMETERS){
                response_code = "07";
            }
            else if (ssp_response == SSP_RESPONSE_CHECKSUM_ERROR){
                response_code = "08";
            }
            else if (ssp_response == SSP_RESPONSE_HEADER_FAILURE){
                response_code = "09";
            }
            else if (ssp_response == SSP_RESPONSE_TIMEOUT){
                response_code = "50";
            }
        }
    }
    char response[19];
    strcpy (response, "0014RS");
    strcat (response, dev_code);
    strcat (response, cmd_code);
    strcat (response, response_code);
    strcat (response, trx_id);
    response[18]='\0';
    write(sock,response,18);
}
