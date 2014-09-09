#ifndef _EXT_DLL_H
#define _EXT_DLL_H

extern "C" __declspec(dllimport) BOOL EnablePort(char* port, unsigned char size, unsigned char parity
                                  ,unsigned char stopbit, DWORD baudrate, unsigned char control);
extern "C" __declspec(dllimport) BOOL DisablePort();
extern "C" __declspec(dllimport) int exe_cmd(BYTE pb_cmd);
extern "C" __declspec(dllimport) int chk_res(unsigned char *pbp_stat, unsigned char * pbp_prc_no, int *pi_errno);
extern "C" __declspec(dllexport) void exe_stop();
extern "C" __declspec(dllimport) void call_src_ver();

extern "C" __declspec(dllimport) int clr();
extern "C" __declspec(dllimport) int rqt();
extern "C" __declspec(dllimport) int issue(BYTE pb_stk);
extern "C" __declspec(dllimport) int set_baudrate(BYTE pb_pm);
//extern "C" __declspec(dllimport) int set_issue(BYTE pb_pm);

extern "C" __declspec(dllimport) int card_capture();
//extern "C" __declspec(dllimport) int feed(BYTE pb_pm);
//extern "C" __declspec(dllimport) int card_stop();
extern "C" __declspec(dllimport) int card_wait(BYTE pb_pm);
extern "C" __declspec(dllimport) int fw_version();

#endif
