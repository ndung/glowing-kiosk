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

#endif
