extern "C" int _fltused = 0;

typedef unsigned char u8;
typedef unsigned short u16;
typedef unsigned int u32;
typedef unsigned long DWORD;
typedef long HRESULT;
typedef int BOOL;
typedef unsigned int UINT;
typedef unsigned long SIZE_T;
typedef void* HANDLE;
typedef void* HMODULE;
typedef void* HWND;
typedef const char* LPCSTR;
typedef char* LPSTR;
typedef void* LPVOID;
typedef long LONG;

#define TRUE 1
#define FALSE 0
#define DLL_PROCESS_ATTACH 1
#define GENERIC_READ 0x80000000u
#define GENERIC_WRITE 0x40000000u
#define FILE_SHARE_READ 0x00000001u
#define FILE_SHARE_WRITE 0x00000002u
#define OPEN_EXISTING 3u
#define OPEN_ALWAYS 4u
#define FILE_ATTRIBUTE_NORMAL 0x00000080u
#define FILE_BEGIN 0u
#define FILE_END 2u
#define INVALID_HANDLE_VALUE ((HANDLE)(long)-1)
#define MEM_COMMIT 0x1000u
#define MEM_RESERVE 0x2000u
#define MEM_RELEASE 0x8000u
#define PAGE_NOACCESS 0x01u
#define PAGE_READWRITE 0x04u
#define PAGE_WRITECOPY 0x08u
#define PAGE_GUARD 0x100u
#define PAGE_EXECUTE_READWRITE 0x40u
#define PAGE_EXECUTE_WRITECOPY 0x80u

#define D3DFMT_A8R8G8B8 21
#define D3DPOOL_MANAGED 1
#define D3DPOOL_SYSTEMMEM 2
#define D3DBACKBUFFER_TYPE_MONO 0
#define D3DPT_TRIANGLESTRIP 5
#define D3DFVF_XYZRHW 0x000004u
#define D3DFVF_DIFFUSE 0x000040u
#define D3DFVF_TEX1 0x000100u
#define D3DFVF_LVSCE (D3DFVF_XYZRHW | D3DFVF_DIFFUSE | D3DFVF_TEX1)
#define D3DSBT_ALL 1

struct LIST_ENTRY32 { LIST_ENTRY32* Flink; LIST_ENTRY32* Blink; };
struct UNICODE_STRING32 { u16 Length; u16 MaximumLength; u16* Buffer; };
struct LDR_DATA_TABLE_ENTRY32 {
    LIST_ENTRY32 InLoadOrderLinks;
    LIST_ENTRY32 InMemoryOrderLinks;
    LIST_ENTRY32 InInitializationOrderLinks;
    void* DllBase;
    void* EntryPoint;
    u32 SizeOfImage;
    UNICODE_STRING32 FullDllName;
    UNICODE_STRING32 BaseDllName;
};
struct PEB_LDR_DATA32 { u32 Length; u8 Initialized; u8 pad1[3]; void* SsHandle; LIST_ENTRY32 InLoadOrderModuleList; LIST_ENTRY32 InMemoryOrderModuleList; LIST_ENTRY32 InInitializationOrderModuleList; };
struct PEB32 { u8 pad0[8]; void* ImageBaseAddress; PEB_LDR_DATA32* Ldr; };
struct IMAGE_DOS_HEADER_LVS { u16 e_magic; u8 pad[58]; u32 e_lfanew; };
struct IMAGE_FILE_HEADER_LVS { u16 Machine; u16 NumberOfSections; u32 TimeDateStamp; u32 PointerToSymbolTable; u32 NumberOfSymbols; u16 SizeOfOptionalHeader; u16 Characteristics; };
struct IMAGE_DATA_DIRECTORY_LVS { u32 VirtualAddress; u32 Size; };
struct IMAGE_OPTIONAL_HEADER32_LVS {
    u16 Magic; u8 MajorLinkerVersion; u8 MinorLinkerVersion; u32 SizeOfCode; u32 SizeOfInitializedData; u32 SizeOfUninitializedData; u32 AddressOfEntryPoint; u32 BaseOfCode; u32 BaseOfData; u32 ImageBase; u32 SectionAlignment; u32 FileAlignment; u16 MajorOperatingSystemVersion; u16 MinorOperatingSystemVersion; u16 MajorImageVersion; u16 MinorImageVersion; u16 MajorSubsystemVersion; u16 MinorSubsystemVersion; u32 Win32VersionValue; u32 SizeOfImage; u32 SizeOfHeaders; u32 CheckSum; u16 Subsystem; u16 DllCharacteristics; u32 SizeOfStackReserve; u32 SizeOfStackCommit; u32 SizeOfHeapReserve; u32 SizeOfHeapCommit; u32 LoaderFlags; u32 NumberOfRvaAndSizes; IMAGE_DATA_DIRECTORY_LVS DataDirectory[16];
};
struct IMAGE_NT_HEADERS32_LVS { u32 Signature; IMAGE_FILE_HEADER_LVS FileHeader; IMAGE_OPTIONAL_HEADER32_LVS OptionalHeader; };
struct IMAGE_EXPORT_DIRECTORY_LVS { u32 Characteristics; u32 TimeDateStamp; u16 MajorVersion; u16 MinorVersion; u32 Name; u32 Base; u32 NumberOfFunctions; u32 NumberOfNames; u32 AddressOfFunctions; u32 AddressOfNames; u32 AddressOfNameOrdinals; };
struct MEMORY_BASIC_INFORMATION_LVS { void* BaseAddress; void* AllocationBase; u32 AllocationProtect; SIZE_T RegionSize; u32 State; u32 Protect; u32 Type; };
struct D3DVIEWPORT9_LVS { u32 X; u32 Y; u32 Width; u32 Height; float MinZ; float MaxZ; };
struct D3DLOCKED_RECT_LVS { int Pitch; void* pBits; };
struct D3DSURFACE_DESC_LVS { int Format; int Type; DWORD Usage; int Pool; int MultiSampleType; DWORD MultiSampleQuality; UINT Width; UINT Height; };
struct RECT_LVS { LONG left; LONG top; LONG right; LONG bottom; };
struct Vertex { float x; float y; float z; float rhw; u32 color; float u; float v; };
struct Texture { void* tex; int width; int height; int loaded; int failed; };

extern "C" void* memset(void* dst, int c, unsigned int n) { u8* d=(u8*)dst; for (unsigned int i=0;i<n;i++) d[i]=(u8)c; return dst; }
extern "C" void* memcpy(void* dst, const void* src, unsigned int n) { u8* d=(u8*)dst; const u8* s=(const u8*)src; for (unsigned int i=0;i<n;i++) d[i]=s[i]; return dst; }
extern "C" int memcmp(const void* a, const void* b, unsigned int n) { const u8* x=(const u8*)a; const u8* y=(const u8*)b; for (unsigned int i=0;i<n;i++){ if(x[i]!=y[i]) return (int)x[i]-(int)y[i]; } return 0; }
static unsigned int StrLen(const char* s){ unsigned int n=0; if(!s) return 0; while(s[n]) n++; return n; }
static int StrEq(const char* a, const char* b){ if(!a||!b) return 0; unsigned int i=0; while(a[i]&&b[i]){ if(a[i]!=b[i]) return 0; i++; } return a[i]==0 && b[i]==0; }
static char ToUpperA(char c){ return (c>='a'&&c<='z') ? (char)(c-32) : c; }

static void* ReadFs30(){ void* p; __asm__ __volatile__("movl %%fs:0x30, %0" : "=r"(p)); return p; }

typedef SIZE_T (__stdcall *VirtualQuery_t)(const void*, MEMORY_BASIC_INFORMATION_LVS*, SIZE_T);
typedef BOOL (__stdcall *VirtualProtect_t)(void*, SIZE_T, u32, u32*);
typedef HANDLE (__stdcall *CreateFileA_t)(LPCSTR, u32, u32, void*, u32, u32, HANDLE);
typedef BOOL (__stdcall *WriteFile_t)(HANDLE, const void*, u32, u32*, void*);
typedef BOOL (__stdcall *ReadFile_t)(HANDLE, void*, u32, u32*, void*);
typedef BOOL (__stdcall *CloseHandle_t)(HANDLE);
typedef u32 (__stdcall *GetFileSize_t)(HANDLE, u32*);
typedef u32 (__stdcall *SetFilePointer_t)(HANDLE, LONG, LONG*, u32);
typedef void* (__stdcall *VirtualAlloc_t)(void*, SIZE_T, u32, u32);
typedef BOOL (__stdcall *VirtualFree_t)(void*, SIZE_T, u32);
typedef u32 (__stdcall *GetTickCount_t)(void);
typedef short (__stdcall *GetAsyncKeyState_t)(int);

static VirtualQuery_t pVirtualQuery=0;
static VirtualProtect_t pVirtualProtect=0;
static CreateFileA_t pCreateFileA=0;
static WriteFile_t pWriteFile=0;
static ReadFile_t pReadFile=0;
static CloseHandle_t pCloseHandle=0;
static GetFileSize_t pGetFileSize=0;
static SetFilePointer_t pSetFilePointer=0;
static VirtualAlloc_t pVirtualAlloc=0;
static VirtualFree_t pVirtualFree=0;
static GetTickCount_t pGetTickCount=0;
static GetAsyncKeyState_t pGetAsyncKeyState=0;

static int g_importsResolved=0;
static int g_installStarted=0;
static int g_installed=0;
static int g_status=0;
static int g_hooked=0;
static int g_texturesLoaded=0;
static int g_firstFrameLogged=0;
static int g_textureFailLogged=0;
static int g_mode=0;
static int g_vehicleHandle=0;
static float g_speedMps=0.0f;
static float g_fuelPercent=-1.0f;
static int g_lowFuel=0;
static float g_rpm=-1.0f;
static int g_gear=-99;
static float g_heading=0.0f;
static float g_altitude=0.0f;
static int g_engineOn=-1;
static int g_checkEngineOn=-1;
static int g_lightsOn=-1;
static int g_highBeamOn=-1;
static int g_leftIndicatorOn=-1;
static int g_rightIndicatorOn=-1;
static float g_opacity=1.0f;
static DWORD g_lastSubmitTick=0;
static float g_lastSignedSpeedMps=0.0f;
static int g_highBeamVehicleAddress=0;
static int g_reverseLampVehicleAddress=0;
static int g_reverseLampState=0;
static DWORD g_reverseLampHoldUntil=0;
static DWORD g_lastTachTick=0;
static int g_rawGearHandle=0;
static int g_rawGearLastForward=0;
static int g_submitLastVehicleHandleForGear=0;
static DWORD g_rawGearLastForwardTick=0;
static DWORD g_rawGearReverseLatchUntil=0;
static DWORD g_rawGearForwardCandidateSince=0;
static DWORD g_tachZeroRpmSince=0;

static DWORD g_layoutLastMoveTick=0;
static DWORD g_layoutLastAdjustLogTick=0;
static short g_layoutPrevSaveDown=0;
static short g_layoutPrevResetDown=0;
static short g_layoutPrevPartDown=0;
static int g_layoutHelpLogged=0;

#define DASH_LAYOUT_MODE_MAX 5
#define DASH_LAYOUT_PART_MAX 4
#define DASH_LAYOUT_PART_ALL 0
#define DASH_LAYOUT_PART_SPEED 1
#define DASH_LAYOUT_PART_TACH 2
#define DASH_LAYOUT_PART_FUEL 3
static float g_layoutPartOffsetX[DASH_LAYOUT_MODE_MAX][DASH_LAYOUT_PART_MAX];
static float g_layoutPartOffsetY[DASH_LAYOUT_MODE_MAX][DASH_LAYOUT_PART_MAX];
static float g_layoutPartScale[DASH_LAYOUT_MODE_MAX][DASH_LAYOUT_PART_MAX];
static float g_layoutDefaultOffsetX[DASH_LAYOUT_MODE_MAX][DASH_LAYOUT_PART_MAX];
static float g_layoutDefaultOffsetY[DASH_LAYOUT_MODE_MAX][DASH_LAYOUT_PART_MAX];
static float g_layoutDefaultScale[DASH_LAYOUT_MODE_MAX][DASH_LAYOUT_PART_MAX];
static int g_layoutSelectedPart[DASH_LAYOUT_MODE_MAX];
static int g_layoutDefaultsApplied=0;
static int g_layoutTunerEnabled=0;
static int g_layoutTunerIniEnabled=0;
static int g_layoutTunerManualEnabled=0;
static short g_layoutPrevToggleDown=0;
static int g_layoutSaveRequested=0;

static void DashboardLayoutSetDefault(int mode, int part, float x, float y, float scale){
    if(mode<0 || mode>=DASH_LAYOUT_MODE_MAX || part<0 || part>=DASH_LAYOUT_PART_MAX) return;
    g_layoutDefaultOffsetX[mode][part]=x;
    g_layoutDefaultOffsetY[mode][part]=y;
    g_layoutDefaultScale[mode][part]=scale;
    g_layoutPartOffsetX[mode][part]=x;
    g_layoutPartOffsetY[mode][part]=y;
    g_layoutPartScale[mode][part]=scale;
}

static void DashboardLayoutEnsureDefaults(){
    if(g_layoutDefaultsApplied) return;
    for(int mi=0; mi<DASH_LAYOUT_MODE_MAX; mi++){
        for(int pi=0; pi<DASH_LAYOUT_PART_MAX; pi++){
            g_layoutDefaultOffsetX[mi][pi]=0.0f;
            g_layoutDefaultOffsetY[mi][pi]=0.0f;
            g_layoutDefaultScale[mi][pi]=1.0f;
            g_layoutPartOffsetX[mi][pi]=0.0f;
            g_layoutPartOffsetY[mi][pi]=0.0f;
            g_layoutPartScale[mi][pi]=1.0f;
        }
        g_layoutSelectedPart[mi]=0;
    }

    /* Shipped large-element placement. Only these elements remain user-adjustable. */
    DashboardLayoutSetDefault(1,DASH_LAYOUT_PART_ALL,0.05000f,-0.14259f,1.02f);
    DashboardLayoutSetDefault(1,DASH_LAYOUT_PART_SPEED,0.015625f,0.00000f,1.00f);
    DashboardLayoutSetDefault(1,DASH_LAYOUT_PART_TACH,0.016667f,0.042593f,0.89f);
    DashboardLayoutSetDefault(1,DASH_LAYOUT_PART_FUEL,0.00000f,0.007407f,1.00f);

    DashboardLayoutSetDefault(2,DASH_LAYOUT_PART_ALL,0.07812f,-0.14258f,0.96f);
    DashboardLayoutSetDefault(2,DASH_LAYOUT_PART_SPEED,0.00000f,-0.01111f,1.00f);
    DashboardLayoutSetDefault(2,DASH_LAYOUT_PART_FUEL,0.00000f,0.00000f,1.00f);

    DashboardLayoutSetDefault(3,DASH_LAYOUT_PART_ALL,0.06979f,-0.13519f,1.12f);
    DashboardLayoutSetDefault(3,DASH_LAYOUT_PART_SPEED,0.00208f,0.02592f,1.00f);
    DashboardLayoutSetDefault(3,DASH_LAYOUT_PART_FUEL,-0.15416f,-0.02592f,0.87f);

    DashboardLayoutSetDefault(4,DASH_LAYOUT_PART_ALL,0.066667f,-0.137037f,1.00f);
    DashboardLayoutSetDefault(4,DASH_LAYOUT_PART_TACH,-0.011458f,0.042593f,0.89f);
    DashboardLayoutSetDefault(4,DASH_LAYOUT_PART_FUEL,0.016667f,0.007407f,1.00f);
    g_layoutDefaultsApplied=1;
}

static int g_tachLastVehicleAddress=0;
static int g_tachPrevEngineOff=1;
static DWORD g_tachStartSurgeUntilTick=0;
static int g_tachInferredEngineOff=0;
static int g_tachEngineStarted=0;
static float g_tachSmoothedFrac=0.18f;
static float g_tachVelocityFrac=0.0f;
static int g_lastTachVehicleHandle=0;
static float g_lastTachSpeedMps=0.0f;

static int g_ffTurnHandle=0;
static int g_ffTurnCurrentBlinker=0;
static int g_ffTurnLastDirection=0;
static int g_ffTurnIsTurning=0;
static int g_ffTurnBlinkersActive=0;
static int g_ffTurnShouldStop=0;
static int g_ffTurnBlinkState=0;
static float g_ffTurnStartTimer=0.0f;
static float g_ffTurnStopTimer=0.0f;
static float g_ffTurnBlinkTimer=0.0f;
static float g_ffTurnFinalBlinkTimer=0.0f;
static DWORD g_ffTurnLastTick=0;

static void* g_device=0;
static void* g_originalEndScene=0;
static void* g_hookedSlotAddress=0;
static u32 g_oldProtect=0;
static Texture g_speedBg={0,0,0,0,0};
static Texture g_tachometerBg={0,0,0,0,0};
static Texture g_bikeTachometerBg={0,0,0,0,0};
static Texture g_needle={0,0,0,0,0};
static Texture g_fuelBg={0,0,0,0,0};
static Texture g_fuelFill={0,0,0,0,0};
static Texture g_bikeFuelBg={0,0,0,0,0};
static Texture g_bikeFuelFill={0,0,0,0,0};
static Texture g_marineSpeedBg={0,0,0,0,0};
static Texture g_marineFuelBg={0,0,0,0,0};
static Texture g_marineFuelFill={0,0,0,0,0};
static Texture g_compassBg={0,0,0,0,0};
static Texture g_aircraftSpeedBg={0,0,0,0,0};
static Texture g_aircraftFuelBg={0,0,0,0,0};
static Texture g_lowOff={0,0,0,0,0};
static Texture g_lowOn={0,0,0,0,0};
static Texture g_engineOffTex={0,0,0,0,0};
static Texture g_engineOnTex={0,0,0,0,0};
static Texture g_highBeamOffTex={0,0,0,0,0};
static Texture g_highBeamOnTex={0,0,0,0,0};
static Texture g_turnLeftOffTex={0,0,0,0,0};
static Texture g_turnLeftOnTex={0,0,0,0,0};
static Texture g_turnRightOffTex={0,0,0,0,0};
static Texture g_turnRightOnTex={0,0,0,0,0};

static const char* LOG_PATH="scripts\\LibertyVehicleServicesCE.log";

static void AppendStr(char* dst, unsigned int* pos, const char* s, unsigned int cap){ if(!s) return; while(*s && *pos+1<cap){ dst[*pos]=*s; (*pos)++; s++; } dst[*pos]=0; }
static void AppendHex8(char* dst, unsigned int* pos, u32 v, unsigned int cap){ static const char* h="0123456789ABCDEF"; AppendStr(dst,pos,"0x",cap); for(int i=7;i>=0;i--){ if(*pos+1<cap){ dst[*pos]=h[(v>>(i*4))&0xF]; (*pos)++; dst[*pos]=0; } } }
static void AppendUInt(char* dst, unsigned int* pos, u32 v, unsigned int cap){ char tmp[16]; int n=0; if(v==0){ AppendStr(dst,pos,"0",cap); return; } while(v && n<15){ tmp[n++]=(char)('0'+(v%10)); v/=10; } for(int i=n-1;i>=0;i--){ if(*pos+1<cap){ dst[*pos]=tmp[i]; (*pos)++; dst[*pos]=0; } } }
static void LogRaw(const char* s){
    if(!g_importsResolved || !pCreateFileA || !pWriteFile || !pCloseHandle || !pSetFilePointer) return;
    HANDLE f=pCreateFileA(LOG_PATH, GENERIC_WRITE, FILE_SHARE_READ|FILE_SHARE_WRITE, 0, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, 0);
    if(f==INVALID_HANDLE_VALUE || !f) return;
    pSetFilePointer(f,0,0,FILE_END);
    static const char prefix[]="[DashboardBridge] ";
    u32 written=0;
    pWriteFile(f,prefix,(u32)(sizeof(prefix)-1),&written,0);
    pWriteFile(f,s,StrLen(s),&written,0);
    const char crlf[2]={'\r','\n'};
    pWriteFile(f,crlf,2,&written,0);
    pCloseHandle(f);
}
static void LogPtr(const char* prefix, u32 ptr){ char b[160]; unsigned int p=0; AppendStr(b,&p,prefix,160); AppendHex8(b,&p,ptr,160); LogRaw(b); }
static void LogUInt(const char* prefix, u32 v){ char b[160]; unsigned int p=0; AppendStr(b,&p,prefix,160); AppendUInt(b,&p,v,160); LogRaw(b); }
static void AppendInt(char* dst, unsigned int* pos, int v, unsigned int cap){ if(v<0){ AppendStr(dst,pos,"-",cap); v=-v; } AppendUInt(dst,pos,(u32)v,cap); }
static void AppendFloat2(char* dst, unsigned int* pos, float v, unsigned int cap){
    if(v<0.0f){ AppendStr(dst,pos,"-",cap); v=-v; }
    int ip=(int)v; AppendUInt(dst,pos,(u32)ip,cap); AppendStr(dst,pos,".",cap);
    int fp=(int)((v-(float)ip)*100.0f+0.5f); if(fp<0) fp=0; if(fp>99) fp=99;
    if(fp<10) AppendStr(dst,pos,"0",cap); AppendUInt(dst,pos,(u32)fp,cap);
}
static void LogDashboardLine(const char* s){
    LogRaw(s);
}

static int UnicodeEqualsAsciiInsensitive(const UNICODE_STRING32& u, const char* ascii){
    if(!u.Buffer || !ascii) return 0;
    unsigned int n=StrLen(ascii); if(u.Length/2 != n) return 0;
    for(unsigned int i=0;i<n;i++){ char c=(char)(u.Buffer[i] & 0xFF); if(ToUpperA(c)!=ToUpperA(ascii[i])) return 0; }
    return 1;
}
static void* GetModuleBaseByName(const char* name){
    PEB32* peb=(PEB32*)ReadFs30(); if(!peb||!peb->Ldr) return 0;
    LIST_ENTRY32* head=&peb->Ldr->InMemoryOrderModuleList; LIST_ENTRY32* cur=head->Flink;
    int guard=0;
    while(cur && cur!=head && guard++<128){
        LDR_DATA_TABLE_ENTRY32* e=(LDR_DATA_TABLE_ENTRY32*)((u8*)cur-8);
        if(e->DllBase && UnicodeEqualsAsciiInsensitive(e->BaseDllName,name)) return e->DllBase;
        cur=cur->Flink;
    }
    return 0;
}
static void* GetMainModuleBase(){ PEB32* peb=(PEB32*)ReadFs30(); return peb ? peb->ImageBaseAddress : 0; }
static void* Rva(void* base, u32 rva){ return (void*)((u8*)base+rva); }
static void* GetExport(void* module, const char* name){
    if(!module || !name) return 0;
    IMAGE_DOS_HEADER_LVS* dos=(IMAGE_DOS_HEADER_LVS*)module; if(dos->e_magic!=0x5A4D) return 0;
    IMAGE_NT_HEADERS32_LVS* nt=(IMAGE_NT_HEADERS32_LVS*)((u8*)module+dos->e_lfanew); if(nt->Signature!=0x00004550) return 0;
    u32 expRva=nt->OptionalHeader.DataDirectory[0].VirtualAddress; if(!expRva) return 0;
    IMAGE_EXPORT_DIRECTORY_LVS* exp=(IMAGE_EXPORT_DIRECTORY_LVS*)Rva(module,expRva);
    u32* names=(u32*)Rva(module,exp->AddressOfNames); u16* ords=(u16*)Rva(module,exp->AddressOfNameOrdinals); u32* funcs=(u32*)Rva(module,exp->AddressOfFunctions);
    for(u32 i=0;i<exp->NumberOfNames;i++){
        char* n=(char*)Rva(module,names[i]);
        if(StrEq(n,name)){ u16 ord=ords[i]; return Rva(module,funcs[ord]); }
    }
    return 0;
}
static int ResolveImports(){
    if(g_importsResolved) return 1;
    void* k=GetModuleBaseByName("KERNEL32.DLL"); if(!k) return 0;
    pVirtualQuery=(VirtualQuery_t)GetExport(k,"VirtualQuery");
    pVirtualProtect=(VirtualProtect_t)GetExport(k,"VirtualProtect");
    pCreateFileA=(CreateFileA_t)GetExport(k,"CreateFileA");
    pWriteFile=(WriteFile_t)GetExport(k,"WriteFile");
    pReadFile=(ReadFile_t)GetExport(k,"ReadFile");
    pCloseHandle=(CloseHandle_t)GetExport(k,"CloseHandle");
    pGetFileSize=(GetFileSize_t)GetExport(k,"GetFileSize");
    pSetFilePointer=(SetFilePointer_t)GetExport(k,"SetFilePointer");
    pVirtualAlloc=(VirtualAlloc_t)GetExport(k,"VirtualAlloc");
    pVirtualFree=(VirtualFree_t)GetExport(k,"VirtualFree");
    pGetTickCount=(GetTickCount_t)GetExport(k,"GetTickCount");
    void* u=GetModuleBaseByName("USER32.DLL");
    if(u) pGetAsyncKeyState=(GetAsyncKeyState_t)GetExport(u,"GetAsyncKeyState");
    if(!pVirtualQuery||!pVirtualProtect||!pCreateFileA||!pWriteFile||!pReadFile||!pCloseHandle||!pGetFileSize||!pSetFilePointer||!pVirtualAlloc||!pVirtualFree||!pGetTickCount) return 0;
    g_importsResolved=1;
    return 1;
}
static int MemoryReadable(const void* p, unsigned int bytes){
    if(!pVirtualQuery || !p) return 0;
    MEMORY_BASIC_INFORMATION_LVS mbi; if(!pVirtualQuery(p,&mbi,sizeof(mbi))) return 0;
    if(mbi.State!=MEM_COMMIT) return 0;
    if(mbi.Protect & PAGE_GUARD) return 0;
    if((mbi.Protect & 0xFF)==PAGE_NOACCESS) return 0;
    u32 start=(u32)p; u32 end=start+bytes;
    u32 regionEnd=(u32)mbi.BaseAddress+(u32)mbi.RegionSize;
    return end>=start && end<=regionEnd;
}
static int GetMainSize(void* base, u32* size){
    if(!base||!size) return 0;
    IMAGE_DOS_HEADER_LVS* dos=(IMAGE_DOS_HEADER_LVS*)base; if(dos->e_magic!=0x5A4D) return 0;
    IMAGE_NT_HEADERS32_LVS* nt=(IMAGE_NT_HEADERS32_LVS*)((u8*)base+dos->e_lfanew); if(nt->Signature!=0x00004550) return 0;
    *size=nt->OptionalHeader.SizeOfImage; return *size>0;
}
static int PatternMatch(const u8* at, const u8* pat, const char* mask, int len){ for(int i=0;i<len;i++){ if(mask[i]=='x' && at[i]!=pat[i]) return 0; } return 1; }
static int AddCandidate(u32* list, int* count, u32 value){
    if(!value || value<0x10000u) return 0;
    for(int i=0;i<*count;i++) if(list[i]==value) return 0;
    if(*count>=16) return 0;
    list[*count]=value; (*count)++; return 1;
}
static int FindDeviceGlobals(u32* list, int* count){
    *count=0;
    void* base=GetMainModuleBase(); if(!base){ LogRaw("DASH_RENDERER_PATTERN_SCAN_NO_MAIN_MODULE"); return 0; }
    u32 size=0; if(!GetMainSize(base,&size)){ LogRaw("DASH_RENDERER_PATTERN_SCAN_BAD_MAIN_MODULE"); return 0; }
    static const u8 pat[13]={0x83,0xC4,0x0C,0xA1,0,0,0,0,0xA3,0,0,0,0};
    static const char mask[14]="xxxx????x????";
    u8* b=(u8*)base;
    for(u32 i=0;i+13<size;i++){
        if(PatternMatch(b+i,pat,mask,13)){
            u32 a=*(u32*)(b+i+4); u32 c=*(u32*)(b+i+9);
            AddCandidate(list,count,a); AddCandidate(list,count,c);
        }
    }
    LogUInt("DASH_RENDERER_PATTERN_GLOBALS ",(u32)*count);
    for(int i=0;i<*count;i++) LogPtr("DASH_RENDERER_PATTERN_GLOBAL ",list[i]);
    return *count>0;
}
static int ValidateDevice(void* dev){
    if(!MemoryReadable(dev,4)) return 0;
    void** vt=*(void***)dev;
    if(!MemoryReadable(vt,43*4)) return 0;
    if(!MemoryReadable(vt[42],16)) return 0;
    return 1;
}

typedef HRESULT (__stdcall *EndScene_t)(void*);
static HRESULT __stdcall HookedEndScene(void* device);

static int HookEndScene(void* dev){
    if(!ValidateDevice(dev)) return 0;
    void** vt=*(void***)dev;
    void** slot=&vt[42];
    if(*slot==(void*)&HookedEndScene) { g_hooked=1; g_device=dev; return 1; }
    u32 old=0;
    if(!pVirtualProtect(slot,4,PAGE_EXECUTE_READWRITE,&old)){ LogRaw("DASH_RENDERER_ENDSCENE_VTABLE_PROTECT_FAILED"); return 0; }
    g_originalEndScene=*slot;
    *slot=(void*)&HookedEndScene;
    u32 dummy=0; pVirtualProtect(slot,4,old,&dummy);
    g_hookedSlotAddress=slot; g_oldProtect=old; g_hooked=1; g_device=dev; g_status|=2;
    LogPtr("DASH_RENDERER_ENDSCENE_VTABLE_HOOKED ",(u32)g_originalEndScene);
    return 1;
}
static int InstallRenderer(){
    if(g_installed) return 1;
    if(!ResolveImports()) return 0;
    LogRaw("DASH_RENDERER_INSTALL_START version=106");
    u32 candidates[16]; int count=0;
    if(!FindDeviceGlobals(candidates,&count)){ LogRaw("DASH_RENDERER_INSTALL_FAILED"); return 0; }
    for(int i=0;i<count;i++){
        u32 global=candidates[i];
        if(!MemoryReadable((void*)global,4)) continue;
        void* dev=*(void**)global;
        if(!ValidateDevice(dev)) continue;
        if(HookEndScene(dev)){
            LogPtr("DASH_RENDERER_DEVICE_GLOBAL_USED ",global);
            LogPtr("DASH_RENDERER_DEVICE_USED ",(u32)dev);
            g_installed=1; g_status|=1;
            LogRaw("DASH_RENDERER_INSTALL_OK");
            return 1;
        }
    }
    LogRaw("DASH_RENDERER_PATTERN_DEVICE_NOT_READY");
    LogRaw("DASH_RENDERER_INSTALL_FAILED");
    return 0;
}

static u16 ReadU16(const u8* p){ return (u16)((u32)p[0]|((u32)p[1]<<8)); }

typedef HRESULT (__stdcall *CreateTexture_t)(void*,UINT,UINT,UINT,DWORD,int,int,void**,void*);
typedef HRESULT (__stdcall *LockRect_t)(void*,UINT,D3DLOCKED_RECT_LVS*,const RECT_LVS*,DWORD);
typedef HRESULT (__stdcall *UnlockRect_t)(void*,UINT);
typedef u32 (__stdcall *Release_t)(void*);
typedef HRESULT (__stdcall *GetBackBuffer_t)(void*,UINT,UINT,int,void**);
typedef HRESULT (__stdcall *CreateOffscreenPlainSurface_t)(void*,UINT,UINT,int,int,void**,void*);
typedef HRESULT (__stdcall *GetRenderTargetData_t)(void*,void*,void*);
typedef HRESULT (__stdcall *SurfaceGetDesc_t)(void*,D3DSURFACE_DESC_LVS*);
typedef HRESULT (__stdcall *SurfaceLockRect_t)(void*,D3DLOCKED_RECT_LVS*,const RECT_LVS*,DWORD);
typedef HRESULT (__stdcall *SurfaceUnlockRect_t)(void*);

static int LoadTgaTexture(void* device, const char* path, Texture* out){
    if(!device||!path||!out||out->loaded) return out&&out->loaded;
    HANDLE f=pCreateFileA(path,GENERIC_READ,FILE_SHARE_READ,0,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,0);
    if(f==INVALID_HANDLE_VALUE||!f) { out->failed=1; return 0; }
    u32 size=pGetFileSize(f,0); if(size<18 || size>67108864u){ pCloseHandle(f); out->failed=1; return 0; }
    u8* data=(u8*)pVirtualAlloc(0,size,0x1000u|0x2000u,0x04u);
    if(!data){ pCloseHandle(f); out->failed=1; return 0; }
    u32 got=0; if(!pReadFile(f,data,size,&got,0)||got!=size){ pCloseHandle(f); pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0; }
    pCloseHandle(f);

    u8 idLen=data[0];
    u8 colorMapType=data[1];
    u8 type=data[2];
    int w=ReadU16(data+12);
    int h=ReadU16(data+14);
    int bpp=data[16];
    u8 desc=data[17];
    int bytesPerPixel=bpp/8;
    if(colorMapType!=0 || (type!=2 && type!=10) || w<=0 || h<=0 || (bpp!=24 && bpp!=32)){
        pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0;
    }
    u32 pixelOffset=18u+(u32)idLen;
    if(pixelOffset>size){ pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0; }
    u32 pixelCount=(u32)w*(u32)h;
    if(pixelCount==0 || pixelCount>16777216u){ pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0; }
    u32 decodedBytes=pixelCount*(u32)bytesPerPixel;
    if(decodedBytes/(u32)bytesPerPixel!=pixelCount){ pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0; }
    u8* decoded=(u8*)pVirtualAlloc(0,decodedBytes,0x1000u|0x2000u,0x04u);
    if(!decoded){ pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0; }

    int decodeOk=1;
    if(type==2){
        if(pixelOffset+decodedBytes>size) decodeOk=0;
        else memcpy(decoded,data+pixelOffset,decodedBytes);
    } else {
        u32 srcPos=pixelOffset;
        u32 dstPixel=0;
        while(dstPixel<pixelCount && srcPos<size){
            u8 packet=data[srcPos++];
            u32 count=(u32)(packet&0x7Fu)+1u;
            if(count>pixelCount-dstPixel){ decodeOk=0; break; }
            if(packet&0x80u){
                if(srcPos+(u32)bytesPerPixel>size){ decodeOk=0; break; }
                const u8* px=data+srcPos;
                srcPos+=(u32)bytesPerPixel;
                for(u32 i=0;i<count;i++) memcpy(decoded+(dstPixel+i)*(u32)bytesPerPixel,px,(u32)bytesPerPixel);
                dstPixel+=count;
            } else {
                u32 packetBytes=count*(u32)bytesPerPixel;
                if(srcPos+packetBytes>size){ decodeOk=0; break; }
                memcpy(decoded+dstPixel*(u32)bytesPerPixel,data+srcPos,packetBytes);
                srcPos+=packetBytes;
                dstPixel+=count;
            }
        }
        if(dstPixel!=pixelCount) decodeOk=0;
    }
    if(!decodeOk){ pVirtualFree(decoded,0,MEM_RELEASE); pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0; }

    void** vt=*(void***)device;
    CreateTexture_t CreateTexture=(CreateTexture_t)vt[23];
    void* tex=0;
    if(CreateTexture(device,(UINT)w,(UINT)h,1,0,D3DFMT_A8R8G8B8,D3DPOOL_MANAGED,&tex,0)<0 || !tex){
        pVirtualFree(decoded,0,MEM_RELEASE); pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0;
    }
    void** tvt=*(void***)tex;
    LockRect_t LockRect=(LockRect_t)tvt[19];
    UnlockRect_t UnlockRect=(UnlockRect_t)tvt[20];
    Release_t Release=(Release_t)tvt[2];
    D3DLOCKED_RECT_LVS lr;
    if(LockRect(tex,0,&lr,0,0)<0){
        Release(tex); pVirtualFree(decoded,0,MEM_RELEASE); pVirtualFree(data,0,MEM_RELEASE); out->failed=1; return 0;
    }
    int originTop=(desc & 0x20) ? 1 : 0;
    int originRight=(desc & 0x10) ? 1 : 0;
    for(int y=0;y<h;y++){
        int sy=originTop ? y : (h-1-y);
        u8* dst=(u8*)lr.pBits+(u32)y*(u32)lr.Pitch;
        for(int x=0;x<w;x++){
            int sx=originRight ? (w-1-x) : x;
            const u8* src=decoded+(((u32)sy*(u32)w)+(u32)sx)*(u32)bytesPerPixel;
            u8 b=src[0]; u8 g=src[1]; u8 r=src[2]; u8 a=(bpp==32)?src[3]:255;
            dst[x*4+0]=b; dst[x*4+1]=g; dst[x*4+2]=r; dst[x*4+3]=a;
        }
    }
    UnlockRect(tex,0);
    pVirtualFree(decoded,0,MEM_RELEASE);
    pVirtualFree(data,0,MEM_RELEASE);
    out->tex=tex; out->width=w; out->height=h; out->loaded=1; out->failed=0;
    return 1;
}
static int LoadTextures(void* device){
    if(g_texturesLoaded) return 1;
    int ok=1;
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\speedometer_bg.tga",&g_speedBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\tachometer_bg.tga",&g_tachometerBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\tachometer_bike_bg.tga",&g_bikeTachometerBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\needle_red.tga",&g_needle);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\fuel_gauge_bg.tga",&g_fuelBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\fuel_gauge_fill.tga",&g_fuelFill);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\bike_fuel_gauge.tga",&g_bikeFuelBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\bike_fuel_gauge_fill.tga",&g_bikeFuelFill);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\marine_speedometer_bg.tga",&g_marineSpeedBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\marine_fuel_gauge.tga",&g_marineFuelBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\compass_bg.tga",&g_compassBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\aircraft_speed_bg.tga",&g_aircraftSpeedBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\aircraft_fuel_gauge.tga",&g_aircraftFuelBg);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\marine_fuel_gauge_fill.tga",&g_marineFuelFill);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_low_fuel_off.tga",&g_lowOff);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_low_fuel_on.tga",&g_lowOn);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_engine_off.tga",&g_engineOffTex);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_engine_on.tga",&g_engineOnTex);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_highbeam_off.tga",&g_highBeamOffTex);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_highbeam_on.tga",&g_highBeamOnTex);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_turn_left_off.tga",&g_turnLeftOffTex);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_turn_left_on.tga",&g_turnLeftOnTex);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_turn_right_off.tga",&g_turnRightOffTex);
    ok &= LoadTgaTexture(device,"scripts\\LibertyVehicleServicesCE\\dashboard_runtime\\icon_turn_right_on.tga",&g_turnRightOnTex);
    if(ok){ g_texturesLoaded=1; g_status|=4; LogRaw("DASH_RENDERER_TEXTURES_OK format=rle_tga road bike tachometer marine compass aircraft fuel_fill lowfuel engine light_icons"); return 1; }
    if(!g_textureFailLogged){ g_textureFailLogged=1; LogRaw("DASH_RENDERER_TEXTURES_MISSING_OR_INVALID"); }
    return 0;
}

typedef HRESULT (__stdcall *CreateStateBlock_t)(void*,int,void**);
typedef HRESULT (__stdcall *SetRenderState_t)(void*,u32,u32);
typedef HRESULT (__stdcall *SetTextureStageState_t)(void*,u32,u32,u32);
typedef HRESULT (__stdcall *SetSamplerState_t)(void*,u32,u32,u32);
typedef HRESULT (__stdcall *SetTexture_t)(void*,u32,void*);
typedef HRESULT (__stdcall *SetFVF_t)(void*,u32);
typedef HRESULT (__stdcall *DrawPrimitiveUP_t)(void*,u32,u32,const void*,u32);
typedef HRESULT (__stdcall *GetViewport_t)(void*,D3DVIEWPORT9_LVS*);
typedef HRESULT (__stdcall *ApplyState_t)(void*);
typedef HRESULT (__stdcall *CaptureState_t)(void*);

static u32 AlphaMul(u32 color, float a){ if(a<0.0f)a=0.0f; if(a>1.0f)a=1.0f; u32 alpha=(color>>24)&0xFFu; alpha=(u32)((float)alpha*a+0.5f); return (color & 0x00FFFFFFu) | (alpha<<24); }
static float SnapF(float v){ return (float)((int)(v+0.5f)); }

static float WrapPi(float x){ const float pi=3.14159265f; const float two=6.28318531f; while(x>pi)x-=two; while(x<-pi)x+=two; return x; }
static float SinF(float x){ x=WrapPi(x); float x2=x*x; return x*(1.0f - x2/6.0f + (x2*x2)/120.0f - (x2*x2*x2)/5040.0f); }
static float CosF(float x){ return SinF(x+1.57079633f); }
static float ClampF(float v,float a,float b){ return v<a?a:(v>b?b:v); }

struct DeviceFns { CreateStateBlock_t CreateStateBlock; SetRenderState_t SetRenderState; SetTextureStageState_t SetTextureStageState; SetSamplerState_t SetSamplerState; SetTexture_t SetTexture; SetFVF_t SetFVF; DrawPrimitiveUP_t DrawPrimitiveUP; GetViewport_t GetViewport; };
static void GetFns(void* dev, DeviceFns* f){ void** vt=*(void***)dev; f->CreateStateBlock=(CreateStateBlock_t)vt[59]; f->SetRenderState=(SetRenderState_t)vt[57]; f->SetTextureStageState=(SetTextureStageState_t)vt[67]; f->SetSamplerState=(SetSamplerState_t)vt[69]; f->SetTexture=(SetTexture_t)vt[65]; f->SetFVF=(SetFVF_t)vt[89]; f->DrawPrimitiveUP=(DrawPrimitiveUP_t)vt[83]; f->GetViewport=(GetViewport_t)vt[48]; }
static void PrepareDraw(void* dev, DeviceFns* f){
    f->SetRenderState(dev,7,0);
    f->SetRenderState(dev,14,1);
    f->SetRenderState(dev,15,0);
    f->SetRenderState(dev,19,5);
    f->SetRenderState(dev,20,6);
    f->SetRenderState(dev,22,1);
    f->SetRenderState(dev,23,0);
    f->SetRenderState(dev,27,1);
    f->SetRenderState(dev,28,0);
    f->SetRenderState(dev,137,0);
    f->SetTextureStageState(dev,0,1,4);
    f->SetTextureStageState(dev,0,2,2);
    f->SetTextureStageState(dev,0,3,3);
    f->SetTextureStageState(dev,0,4,4);
    f->SetTextureStageState(dev,0,5,2);
    f->SetTextureStageState(dev,0,6,3);
    f->SetTextureStageState(dev,1,1,1);
    f->SetTextureStageState(dev,1,4,1);
    f->SetSamplerState(dev,0,5,2);
    f->SetSamplerState(dev,0,6,2);
    f->SetSamplerState(dev,0,7,0);
    f->SetFVF(dev,D3DFVF_LVSCE);
}
static void DrawTex(void* dev, DeviceFns* f, Texture* t, float cx, float cy, float w, float h, float angle, u32 color){
    if(!t||!t->loaded||!t->tex) return;
    float hw=w*0.5f, hh=h*0.5f; float c=CosF(angle), s=SinF(angle);
    float xs[4]={-hw, hw, -hw, hw}; float ys[4]={-hh, -hh, hh, hh}; float us[4]={0,1,0,1}; float vs[4]={0,0,1,1}; Vertex v[4];
    for(int i=0;i<4;i++){ float rx=xs[i]*c-ys[i]*s; float ry=xs[i]*s+ys[i]*c; v[i].x=SnapF(cx+rx); v[i].y=SnapF(cy+ry); v[i].z=0.0f; v[i].rhw=1.0f; v[i].color=color; v[i].u=us[i]; v[i].v=vs[i]; }
    f->SetTexture(dev,0,t->tex); f->DrawPrimitiveUP(dev,D3DPT_TRIANGLESTRIP,2,v,sizeof(Vertex));
}
static void DrawTexClipLeft(void* dev, DeviceFns* f, Texture* t, float cx, float cy, float w, float h, float angle, float fillFrac, u32 color){
    if(!t||!t->loaded||!t->tex) return;
    fillFrac=ClampF(fillFrac,0.0f,1.0f);
    if(fillFrac<=0.001f) return;
    float fullHw=w*0.5f, hh=h*0.5f; float visW=w*fillFrac; float hw=visW*0.5f;
    float c=CosF(angle), s=SinF(angle);
    float centerShift=(-fullHw + hw);
    float rcx=cx + c*centerShift; float rcy=cy + s*centerShift;
    float xs[4]={-hw, hw, -hw, hw}; float ys[4]={-hh, -hh, hh, hh}; float us[4]={0,fillFrac,0,fillFrac}; float vs[4]={0,0,1,1}; Vertex v[4];
    for(int i=0;i<4;i++){ float rx=xs[i]*c-ys[i]*s; float ry=xs[i]*s+ys[i]*c; v[i].x=SnapF(rcx+rx); v[i].y=SnapF(rcy+ry); v[i].z=0.0f; v[i].rhw=1.0f; v[i].color=color; v[i].u=us[i]; v[i].v=vs[i]; }
    f->SetTexture(dev,0,t->tex); f->DrawPrimitiveUP(dev,D3DPT_TRIANGLESTRIP,2,v,sizeof(Vertex));
}

static void DrawRotRect(void* dev, DeviceFns* f, float cx, float cy, float w, float h, float angle, u32 color){
    float hw=w*0.5f, hh=h*0.5f; float c=CosF(angle), s=SinF(angle);
    float xs[4]={-hw, hw, -hw, hw}; float ys[4]={-hh, -hh, hh, hh}; Vertex v[4];
    for(int i=0;i<4;i++){ float rx=xs[i]*c-ys[i]*s; float ry=xs[i]*s+ys[i]*c; v[i].x=SnapF(cx+rx); v[i].y=SnapF(cy+ry); v[i].z=0.0f; v[i].rhw=1.0f; v[i].color=color; v[i].u=0.0f; v[i].v=0.0f; }
    f->SetTexture(dev,0,0);
    f->DrawPrimitiveUP(dev,D3DPT_TRIANGLESTRIP,2,v,sizeof(Vertex));
}

static void DrawFuelFillTex(void* dev, DeviceFns* f, Texture* fillTex, float cx, float cy, float size, float fuel, float opacity){
    if(fuel<0.0f) return;
    fuel=ClampF(fuel,0.0f,100.0f);
    float frac=fuel/100.0f;
    // The shipped fuel-fill texture has transparent padding at both ends of the usable coloured span.
    // Map 0..100% to that visible span so 1-4% and 96-99% are no longer visually clamped.
    float visibleStart=0.050f;
    float visibleEnd=0.950f;
    float clipFrac=visibleStart + (visibleEnd-visibleStart)*frac;
    if(frac<=0.001f) clipFrac=0.0f;
    u32 fillColor=AlphaMul(0xFFFFFFFFu, opacity);
    DrawTexClipLeft(dev,f,fillTex,cx,cy,size,size,0.0f,clipFrac,fillColor);
}
static int IsLowFuelAttentionOn(DWORD now){
    // Brief pulse every ~2.4 seconds while fuel is low but not yet critical.
    DWORD t=now%2400u;
    return (t<190u) ? 1 : 0;
}
static int IsLowFuelIndicatorOn(DWORD now){
    if(g_lowFuel==0) return 0;
    if(g_fuelPercent>=0.0f && g_fuelPercent<=5.0f) return 1;
    return IsLowFuelAttentionOn(now);
}
static void RestoreState(void* state){
    if(!state) return;
    void** svt=*(void***)state;
    ApplyState_t Apply=(ApplyState_t)svt[5];
    Release_t Release=(Release_t)svt[2];
    if(Apply) Apply(state);
    if(Release) Release(state);
}
static int FloatFiniteLvs(float v){ u32 b=*(u32*)&v; return (b & 0x7F800000u) != 0x7F800000u; }
static int SafeReadFloatLvs(int addr, float* out){ if(!out) return 0; if(!MemoryReadable((void*)addr,4)) return 0; float v=*(float*)addr; if(!FloatFiniteLvs(v)) return 0; *out=v; return 1; }
static int SafeReadIntLvs(int addr, int* out){ if(!out) return 0; if(!MemoryReadable((void*)addr,4)) return 0; *out=*(int*)addr; return 1; }
static int SafeReadByteLvs(int addr, u8* out){ if(!out) return 0; if(!MemoryReadable((void*)addr,1)) return 0; *out=*(u8*)addr; return 1; }








static void ResetFusionFixTurnTelemetry(int handle, DWORD now){
    g_ffTurnHandle=handle;
    g_ffTurnCurrentBlinker=0;
    g_ffTurnLastDirection=0;
    g_ffTurnIsTurning=0;
    g_ffTurnBlinkersActive=0;
    g_ffTurnShouldStop=0;
    g_ffTurnBlinkState=0;
    g_ffTurnStartTimer=0.0f;
    g_ffTurnStopTimer=0.0f;
    g_ffTurnBlinkTimer=0.0f;
    g_ffTurnFinalBlinkTimer=0.0f;
    g_ffTurnLastTick=now;
}


static void UpdateFusionFixLikeTurnIndicators(int vehicleAddress, int vehicleHandle){
    if(!pGetTickCount || vehicleAddress==0 || vehicleHandle==0) return;
    DWORD now=pGetTickCount();
    if(g_ffTurnHandle!=vehicleHandle || g_ffTurnLastTick==0){ ResetFusionFixTurnTelemetry(vehicleHandle, now); }
    DWORD dtMs=(now>=g_ffTurnLastTick) ? (now-g_ffTurnLastTick) : 0u;
    g_ffTurnLastTick=now;
    if(dtMs>250u) dtMs=250u;
    float dt=(float)dtMs;

    float steer=0.0f;
    int haveSteer=0;
    float s0=0.0f;
    if(SafeReadFloatLvs(vehicleAddress+0x1088,&s0) && s0>-2.2f && s0<2.2f){ steer=s0; haveSteer=1; }
    else if(SafeReadFloatLvs(vehicleAddress+0x10D8,&s0) && s0>-2.2f && s0<2.2f){ steer=s0; haveSteer=1; }
    if(!haveSteer){ return; }

    int currentDir=0;
    if(steer>0.50f) currentDir=1;
    else if(steer<-0.50f) currentDir=2;
    int currentlyTurning=(currentDir!=0) ? 1 : 0;
    int directionChanged=(currentDir!=g_ffTurnLastDirection && currentDir!=0) ? 1 : 0;

    if(directionChanged){
        if(g_ffTurnBlinkersActive){
            g_ffTurnBlinkersActive=0;
            g_ffTurnCurrentBlinker=0;
            g_ffTurnShouldStop=0;
            g_ffTurnBlinkState=0;
            g_ffTurnBlinkTimer=0.0f;
        }
        g_ffTurnStartTimer=0.0f;
        g_ffTurnStopTimer=0.0f;
        g_ffTurnIsTurning=1;
    } else if(currentlyTurning && !g_ffTurnIsTurning){
        g_ffTurnStartTimer=0.0f;
        g_ffTurnStopTimer=0.0f;
        g_ffTurnIsTurning=1;
    } else if(!currentlyTurning && g_ffTurnIsTurning){
        g_ffTurnStopTimer=0.0f;
        g_ffTurnIsTurning=0;
        if(g_ffTurnBlinkersActive){
            g_ffTurnShouldStop=1;
            g_ffTurnFinalBlinkTimer=0.0f;
        }
    }

    if(g_ffTurnIsTurning && currentDir!=0 && !g_ffTurnShouldStop){
        g_ffTurnStartTimer += dt;
        if(g_ffTurnStartTimer>=400.0f && !g_ffTurnBlinkersActive){
            g_ffTurnCurrentBlinker=currentDir;
            g_ffTurnBlinkersActive=1;
            g_ffTurnBlinkTimer=0.0f;
            g_ffTurnBlinkState=1;
        }
    }
    g_ffTurnLastDirection=currentDir;

    if(g_ffTurnBlinkersActive && !g_ffTurnShouldStop){
        g_ffTurnBlinkTimer += dt;
        if(g_ffTurnBlinkTimer>=400.0f){
            g_ffTurnBlinkState = g_ffTurnBlinkState ? 0 : 1;
            g_ffTurnBlinkTimer=0.0f;
        }
    } else if(g_ffTurnShouldStop && g_ffTurnBlinkersActive){
        g_ffTurnFinalBlinkTimer += dt;
        g_ffTurnBlinkTimer += dt;
        if(g_ffTurnBlinkTimer>=400.0f){
            if(g_ffTurnBlinkState){
                g_ffTurnBlinkState=0;
                g_ffTurnBlinkTimer=0.0f;
            } else {
                g_ffTurnBlinkersActive=0;
                g_ffTurnCurrentBlinker=0;
                g_ffTurnShouldStop=0;
                g_ffTurnBlinkState=0;
                g_ffTurnBlinkTimer=0.0f;
            }
        }
        if(g_ffTurnFinalBlinkTimer>=800.0f){
            g_ffTurnBlinkersActive=0;
            g_ffTurnCurrentBlinker=0;
            g_ffTurnShouldStop=0;
            g_ffTurnBlinkState=0;
            g_ffTurnBlinkTimer=0.0f;
        }
    }

    g_leftIndicatorOn=0;
    g_rightIndicatorOn=0;
    if(g_ffTurnBlinkersActive && g_ffTurnCurrentBlinker==1) g_leftIndicatorOn=g_ffTurnBlinkState ? 1 : 0;
    else if(g_ffTurnBlinkersActive && g_ffTurnCurrentBlinker==2) g_rightIndicatorOn=g_ffTurnBlinkState ? 1 : 0;
}


static int IsFrontLightStateOn(int ad0, int b84, int b88, int b8c, int b90){
    // Coarse low/front-light state only. v52 logs show 5/8/13/16/52/59/62/70 as lit/transient lamp values.
    if(ad0>3 || b84>3 || b88>3 || b8c>3 || b90>3) return 1;
    return 0;
}


static void ApplyLightTelemetryFromStates(int ad0, int b84, int b88, int b8c, int b90){
    g_lightsOn=IsFrontLightStateOn(ad0,b84,b88,b8c,b90);
}

static void UpdateHighBeamFromVehicleMemory(int vehicleAddress){
    if(vehicleAddress==0){
        g_highBeamVehicleAddress=0;
        g_highBeamOn=0;
        return;
    }
    g_highBeamVehicleAddress=vehicleAddress;
    u8 raw=0xFF;
    int haveRaw=SafeReadByteLvs(vehicleAddress+0x0F19,&raw);
    g_highBeamOn=(haveRaw && raw==0x03) ? 1 : 0;
}

static int ReadReverseLampCandidateRaw(int vehicleAddress){
    u8 b107B=0, b1090=0, b10A7=0;
    if(!SafeReadByteLvs(vehicleAddress+0x107B,&b107B)) return 0;
    if(!SafeReadByteLvs(vehicleAddress+0x1090,&b1090)) return 0;
    if(!SafeReadByteLvs(vehicleAddress+0x10A7,&b10A7)) return 0;
    return (b107B==0xBF && b1090==0x00 && b10A7==0xBF) ? 1 : 0;
}

static int UpdateReverseLampCandidate(int vehicleAddress, int rawC04){
    if(!pGetTickCount || vehicleAddress==0){
        g_reverseLampVehicleAddress=0;
        g_reverseLampState=0;
        g_reverseLampHoldUntil=0;
        return 0;
    }
    DWORD now=pGetTickCount();
    if(g_reverseLampVehicleAddress!=vehicleAddress){
        g_reverseLampVehicleAddress=vehicleAddress;
        g_reverseLampState=0;
        g_reverseLampHoldUntil=0;
    }
    int rawOn=(rawC04==0 && ReadReverseLampCandidateRaw(vehicleAddress)) ? 1 : 0;
    if(rawOn){
        g_reverseLampState=1;
        g_reverseLampHoldUntil=now+180u;
    } else if(g_reverseLampState && now>g_reverseLampHoldUntil){
        g_reverseLampState=0;
    }
    return g_reverseLampState;
}

static void UpdateGearDisplayFromRawTelemetry(int vehicleHandle, float speedMps, float signedSpeedMps, int rawC04, int fallbackGear, int reverseLampOn){
    DWORD now=pGetTickCount ? pGetTickCount() : 0u;
    if(vehicleHandle==0){
        g_rawGearHandle=0;
        g_rawGearLastForward=0;
        g_rawGearLastForwardTick=0;
        g_rawGearReverseLatchUntil=0;
        g_rawGearForwardCandidateSince=0;
        g_gear=fallbackGear;
        return;
    }
    if(g_rawGearHandle!=vehicleHandle){
        g_rawGearHandle=vehicleHandle;
        g_rawGearLastForward=0;
        g_rawGearLastForwardTick=0;
        g_rawGearReverseLatchUntil=0;
        g_rawGearForwardCandidateSince=0;
    }

    int raw=(rawC04>=0 && rawC04<=8) ? rawC04 : 999;

    // Reverse display is gated by the confirmed reverse-lamp state; signed-speed and fallback gear cannot create R.
    if(reverseLampOn){
        g_rawGearReverseLatchUntil=now+180u;
        g_rawGearForwardCandidateSince=0;
        g_gear=-1;
        return;
    }
    g_rawGearReverseLatchUntil=0;
    g_rawGearForwardCandidateSince=0;

    if(speedMps<0.20f){
        g_gear=0;
        return;
    }

    if(raw>=1 && raw<=8){
        g_gear=raw;
        g_rawGearLastForward=raw;
        g_rawGearLastForwardTick=now;
        return;
    }

    if(raw==0 && g_rawGearLastForward>0 && speedMps>1.0f){
        if(now==0 || g_rawGearLastForwardTick==0 || now-g_rawGearLastForwardTick<6500u || speedMps>8.0f){
            g_gear=g_rawGearLastForward;
            return;
        }
    }

    if(fallbackGear>0){
        g_gear=fallbackGear;
        return;
    }

    // Do not create R from fallbackGear alone. The managed signed-speed fallback has produced
    // false R spikes during normal forward driving; it can only assist a negative signed-speed trigger above.
    if(g_rawGearLastForward>0 && speedMps>1.0f){
        g_gear=g_rawGearLastForward;
        return;
    }

    g_gear=fallbackGear;
}











static void SubmitTelemetryEx(int vehicleAddress, int vehicleHandle, float speedMps, float signedSpeedMps, int gear, int mode){
    if(mode!=1 && mode!=4){ return; }
    if(vehicleAddress==0){ g_gear=gear; g_tachLastVehicleAddress=0; return; }
    g_tachLastVehicleAddress=vehicleAddress;
    g_lastSignedSpeedMps=signedSpeedMps;

    float rpmCandidate=-1.0f;
    int gearRaw=999;
    float f=0.0f;
    int iv=0;

    if(SafeReadFloatLvs(vehicleAddress+0x638,&f) && f>=0.0f && f<=1.25f) rpmCandidate=f;
    else if(SafeReadFloatLvs(vehicleAddress+0xC14,&f) && f>=0.0f && f<=1.25f) rpmCandidate=f;
    else if(SafeReadFloatLvs(vehicleAddress+0xD54,&f) && f>=0.0f && f<=1.25f) rpmCandidate=f;

    if(SafeReadIntLvs(vehicleAddress+0xC04,&iv) && iv>=0 && iv<=8) gearRaw=iv;

    int ad0=0,b84=0,b88=0,b8c=0,b90=0;
    SafeReadIntLvs(vehicleAddress+0xAD0,&ad0);
    SafeReadIntLvs(vehicleAddress+0xB84,&b84);
    SafeReadIntLvs(vehicleAddress+0xB88,&b88);
    SafeReadIntLvs(vehicleAddress+0xB8C,&b8c);
    SafeReadIntLvs(vehicleAddress+0xB90,&b90);
    ApplyLightTelemetryFromStates(ad0,b84,b88,b8c,b90);
    UpdateHighBeamFromVehicleMemory(vehicleAddress);
    UpdateFusionFixLikeTurnIndicators(vehicleAddress, vehicleHandle);
    if(rpmCandidate>=0.0f) g_rpm=rpmCandidate;
    int reverseLampOn=UpdateReverseLampCandidate(vehicleAddress, gearRaw);
    UpdateGearDisplayFromRawTelemetry(vehicleHandle, speedMps, signedSpeedMps, gearRaw, gear, reverseLampOn);

}

static int ShouldSuppressDashboardByStaleFrame(){
    if(!pGetTickCount) return 0;
    DWORD now=pGetTickCount();
    if(g_lastSubmitTick!=0 && now>=g_lastSubmitTick && (now-g_lastSubmitTick)>85u) return 1;
    return 0;
}

static void DrawNeedleForSpeed(void* dev, DeviceFns* f, float speedX, float speedY, float size, float speedMps, float maxMph, u32 texColor){
    float mph=speedMps*2.23693629f;
    float m=ClampF(mph,0.0f,maxMph);
    float angle=(-130.0f+(m/maxMph)*260.0f)*0.01745329252f;
    DrawTex(dev,f,&g_needle,speedX,speedY,size,size,angle,texColor);
}


static int RoundToInt(float v){ return (int)(v + (v>=0.0f ? 0.5f : -0.5f)); }

static void DrawSegmentDigit(void* dev, DeviceFns* f, int digit, float x, float y, float h, u32 color){
    static const unsigned char masks[10] = { 0x3F, 0x06, 0x5B, 0x4F, 0x66, 0x6D, 0x7D, 0x07, 0x7F, 0x6F };
    if(digit<0 || digit>9) return;
    unsigned char m=masks[digit];
    float w=h*0.56f;
    float t=h*0.102f;
    float hw=w*0.5f;
    float q=h*0.25f;
    if(m&0x01) DrawRotRect(dev,f,x,y-h*0.5f,w,t,0.0f,color);
    if(m&0x02) DrawRotRect(dev,f,x+hw-t*0.5f,y-q,t,h*0.5f,0.0f,color);
    if(m&0x04) DrawRotRect(dev,f,x+hw-t*0.5f,y+q,t,h*0.5f,0.0f,color);
    if(m&0x08) DrawRotRect(dev,f,x,y+h*0.5f,w,t,0.0f,color);
    if(m&0x10) DrawRotRect(dev,f,x-hw+t*0.5f,y+q,t,h*0.5f,0.0f,color);
    if(m&0x20) DrawRotRect(dev,f,x-hw+t*0.5f,y-q,t,h*0.5f,0.0f,color);
    if(m&0x40) DrawRotRect(dev,f,x,y,w,t,0.0f,color);
}

static void DrawNumberRightAligned(void* dev, DeviceFns* f, int value, float right, float y, float h, int maxDigits, u32 color){
    if(value<0) value=0;
    int digits[10];
    int count=0;
    if(value==0){ digits[count++]=0; }
    else { while(value>0 && count<10){ digits[count++]=value%10; value/=10; } }
    if(count>maxDigits) count=maxDigits;
    float step=h*0.68f;
    float x=right;
    for(int i=0;i<count;i++){ DrawSegmentDigit(dev,f,digits[i],x,y,h,color); x-=step; }
}

static void DrawNumberCentered(void* dev, DeviceFns* f, int value, float cx, float y, float h, int maxDigits, u32 color){
    if(value<0) value=0;
    int digits[10];
    int count=0;
    if(value==0){ digits[count++]=0; }
    else { while(value>0 && count<10){ digits[count++]=value%10; value/=10; } }
    if(count>maxDigits) count=maxDigits;
    float step=h*0.68f;
    float total=(count>0) ? ((count-1)*step) : 0.0f;
    float x=cx-total*0.5f;
    for(int i=count-1;i>=0;i--){ DrawSegmentDigit(dev,f,digits[i],x,y,h,color); x+=step; }
}

static void DrawTexClipTop(void* dev, DeviceFns* f, Texture* t, float cx, float cy, float w, float h, float angle, float fillFrac, u32 color){
    if(!t||!t->loaded||!t->tex) return;
    fillFrac=ClampF(fillFrac,0.0f,1.0f);
    if(fillFrac<=0.001f) return;
    float hw=w*0.5f, fullHh=h*0.5f, visH=h*fillFrac, hh=visH*0.5f;
    float c=CosF(angle), ss=SinF(angle);
    float centerShift=(-fullHh + hh);
    float rcx=cx - ss*centerShift; float rcy=cy + c*centerShift;
    float xs[4]={-hw, hw, -hw, hw}; float ys[4]={-hh, -hh, hh, hh}; float us[4]={0,1,0,1}; float vs[4]={0,0,fillFrac,fillFrac}; Vertex v[4];
    for(int i=0;i<4;i++){ float rx=xs[i]*c-ys[i]*ss; float ry=xs[i]*ss+ys[i]*c; v[i].x=SnapF(rcx+rx); v[i].y=SnapF(rcy+ry); v[i].z=0.0f; v[i].rhw=1.0f; v[i].color=color; v[i].u=us[i]; v[i].v=vs[i]; }
    f->SetTexture(dev,0,t->tex); f->DrawPrimitiveUP(dev,D3DPT_TRIANGLESTRIP,2,v,sizeof(Vertex));
}

static void DrawBikeFuelFillTex(void* dev, DeviceFns* f, Texture* fillTex, float cx, float cy, float size, float fuel, float opacity){
    if(fuel<0.0f) return;
    fuel=ClampF(fuel,0.0f,100.0f);
    float frac=fuel/100.0f;
    float visibleStart=0.128f;
    float visibleEnd=0.861f;
    float clipFrac=visibleStart + (visibleEnd-visibleStart)*frac;
    if(frac<=0.001f) clipFrac=0.0f;
    u32 fillColor=AlphaMul(0xFFFFFFFFu, opacity);
    DrawTexClipTop(dev,f,fillTex,cx,cy,size,size,0.0f,clipFrac,fillColor);
}

static void DrawGearLetterN(void* dev, DeviceFns* f, float x, float y, float h, u32 color){
    float w=h*0.58f; float t=h*0.105f;
    DrawRotRect(dev,f,x-w*0.45f,y,t,h,0.0f,color);
    DrawRotRect(dev,f,x+w*0.45f,y,t,h,0.0f,color);
    DrawRotRect(dev,f,x,y,h*1.05f,t,0.92f,color);
}
static void DrawGearLetterR(void* dev, DeviceFns* f, float x, float y, float h, u32 color){
    float w=h*0.58f; float t=h*0.105f;
    DrawRotRect(dev,f,x-w*0.45f,y,t,h,0.0f,color);
    DrawRotRect(dev,f,x,y-h*0.48f,w,t,0.0f,color);
    DrawRotRect(dev,f,x,y,w,t,0.0f,color);
    DrawRotRect(dev,f,x+w*0.45f,y-h*0.24f,t,h*0.45f,0.0f,color);
    DrawRotRect(dev,f,x+w*0.22f,y+h*0.28f,h*0.55f,t,0.78f,color);
}
static void DrawGearIndicator(void* dev, DeviceFns* f, int gear, float x, float y, float h, u32 color){
    if(gear==-1) DrawGearLetterR(dev,f,x,y,h,color);
    else if(gear==0 || gear==-99) DrawGearLetterN(dev,f,x,y,h,color);
    else if(gear>0) DrawNumberRightAligned(dev,f,gear,x+h*0.16f,y,h,2,color);
}

static float TachIdleWobble(DWORD now){
    if(now==0) return 0.0f;
    float t=(float)(now & 0xFFFFu);
    return 0.0038f*SinF(t*0.0340f) + 0.0018f*SinF(t*0.0710f);
}

static float TachIdleNeedleTremor(DWORD now){
    if(now==0) return 0.0f;
    float t=(float)(now & 0xFFFFu);
    return 0.0042f*SinF(t*0.0440f) + 0.0018f*SinF(t*0.0920f);
}

static int ReadTachRawMemory(float* raw638, float* rawC14, float* rawD54){
    int ok=0;
    float f=0.0f;
    if(raw638) *raw638=-1.0f;
    if(rawC14) *rawC14=-1.0f;
    if(rawD54) *rawD54=-1.0f;
    int a=g_tachLastVehicleAddress;
    if(a==0) return 0;
    if(raw638 && SafeReadFloatLvs(a+0x638,&f) && f>=0.0f && f<=1.25f){ *raw638=f; ok=1; }
    if(rawC14 && SafeReadFloatLvs(a+0xC14,&f) && f>=0.0f && f<=1.25f){ *rawC14=f; ok=1; }
    if(rawD54 && SafeReadFloatLvs(a+0xD54,&f) && f>=0.0f && f<=1.25f){ *rawD54=f; ok=1; }
    return ok;
}

static float TachRawRpmForNeedle(float rpmTelemetry, int* rawAvailable, float* raw638Out, float* rawC14Out, float* rawD54Out){
    float raw638=-1.0f, rawC14=-1.0f, rawD54=-1.0f;
    int have=ReadTachRawMemory(&raw638,&rawC14,&rawD54);
    if(raw638Out) *raw638Out=raw638;
    if(rawC14Out) *rawC14Out=rawC14;
    if(rawD54Out) *rawD54Out=rawD54;
    if(rawAvailable) *rawAvailable=0;
    if(have && raw638>=0.0f){
        if(rawAvailable) *rawAvailable=1;
        return raw638;
    }
    if(have && rawC14>=0.0f){
        if(rawAvailable) *rawAvailable=1;
        return rawC14;
    }
    if(have && rawD54>=0.0f){
        if(rawAvailable) *rawAvailable=1;
        return rawD54;
    }
    if(rpmTelemetry>=0.0f){
        if(rawAvailable) *rawAvailable=0;
        return rpmTelemetry;
    }
    return -1.0f;
}

static int InferTachEngineOffFromRaw(float speedMps, float rawRpm, float raw638, int gear, DWORD now){
    (void)gear;
    if(g_engineOn==0){
        g_tachInferredEngineOff=1;
        g_tachEngineStarted=0;
        return 1;
    }
    if(g_engineOn>0){
        g_tachInferredEngineOff=0;
        g_tachEngineStarted=1;
        g_tachZeroRpmSince=0;
        return 0;
    }

    // Do not infer engine state from gear or speed; prefer the raw RPM field.
    // In the tested logs 0x638 is the cleanest RPM source: 0.00 for true off, ~0.10 for idle,
    // and higher values under load. C14/D54 can sit at 0.20 while the engine is off, so they are
    // not allowed to keep the tach alive when 0x638 says zero.
    int rawZero=(raw638>=0.0f && raw638<=0.026f) ? 1 : 0;
    int stopped=(speedMps<0.65f) ? 1 : 0;
    if(rawZero && stopped){
        if(g_tachZeroRpmSince==0 && now!=0) g_tachZeroRpmSince=now;
        if(now==0 || g_tachZeroRpmSince==0 || now-g_tachZeroRpmSince>=180u){
            g_tachInferredEngineOff=1;
            g_tachEngineStarted=0;
            return 1;
        }
    } else {
        g_tachZeroRpmSince=0;
    }

    if(rawRpm>0.055f || speedMps>0.85f){
        g_tachInferredEngineOff=0;
        g_tachEngineStarted=1;
        return 0;
    }

    return g_tachInferredEngineOff;
}

static float TachTargetFromRaw(float speedMps, float rawRpm, int gear, DWORD now, int engineOff){
    if(engineOff) return 0.0f;
    float idle=0.100f + TachIdleWobble(now);
    if(rawRpm>=0.0f){
        float mapped=rawRpm;
        if(rawRpm<=1.25f) mapped=0.075f + ClampF(rawRpm,0.0f,1.0f)*0.795f;
        else mapped=ClampF(rawRpm/7000.0f,0.0f,1.0f);
        if((gear==0 || gear==-99) && speedMps<0.70f){
            // Idle still gets a tiny analog wobble, but it is anchored to the real raw RPM.
            float idleMapped=0.075f + ClampF(rawRpm,0.0f,1.0f)*0.795f;
            float w=idle - 0.1545f; // keep old neutral wobble around the 0x638~=0.10 idle mark
            mapped=idleMapped + w;
        }
        return ClampF(mapped,0.0f,0.94f);
    }

    // Last-resort fallback only when raw RPM is unreadable.
    if(gear==0 || gear==-99 || speedMps<0.55f) return idle;
    if(gear<0){
        float r=ClampF(speedMps/11.5f,0.0f,1.0f);
        return ClampF(idle + r*0.48f,0.11f,0.82f);
    }
    float mph=speedMps*2.23693629f;
    float tops[7]={0.0f,31.0f,56.0f,84.0f,112.0f,145.0f,182.0f};
    int g=gear; if(g<1) g=1; if(g>6) g=6;
    float low=(g==1)?0.0f:tops[g-1]*0.52f;
    float high=tops[g];
    float r=ClampF((mph-low)/((high-low)>1.0f?(high-low):1.0f),0.0f,1.0f);
    return ClampF(0.22f + r*0.64f,0.09f,0.93f);
}

static void DrawNeedleForTachApprox(void* dev, DeviceFns* f, float tachX, float tachY, float size, float speedMps, float rpmTelemetry, int gear, u32 texColor){
    DWORD now=0; if(pGetTickCount) now=pGetTickCount();
    float dt=0.016f;
    if(now!=0 && g_lastTachTick!=0 && now>=g_lastTachTick){
        dt=(float)(now-g_lastTachTick)/1000.0f;
        if(dt<0.001f) dt=0.001f;
        if(dt>0.085f) dt=0.085f;
    }
    int vehicleChanged=(g_lastTachVehicleHandle!=g_vehicleHandle || now==0 || g_lastTachTick==0) ? 1 : 0;
    if(vehicleChanged){
        g_tachZeroRpmSince=0;
        g_tachInferredEngineOff=1;
        g_tachEngineStarted=0;
        g_tachPrevEngineOff=1;
        g_tachStartSurgeUntilTick=0;
        g_tachVelocityFrac=0.0f;
        g_tachSmoothedFrac=0.0f;
        g_lastTachVehicleHandle=g_vehicleHandle;
    }

    int rawAvailable=0;
    float raw638=-1.0f, rawC14=-1.0f, rawD54=-1.0f;
    float rawRpm=TachRawRpmForNeedle(rpmTelemetry,&rawAvailable,&raw638,&rawC14,&rawD54);
    int engineOff=InferTachEngineOffFromRaw(speedMps,rawRpm,raw638,gear,now);
    float target=TachTargetFromRaw(speedMps,rawRpm,gear,now,engineOff);

    if(g_tachPrevEngineOff && !engineOff && now!=0 && speedMps<1.05f){
        g_tachStartSurgeUntilTick=now+620u;
    }
    if(!engineOff && now!=0 && g_tachStartSurgeUntilTick>now && speedMps<1.30f){
        float remain=(float)(g_tachStartSurgeUntilTick-now)/620.0f;
        if(remain<0.0f) remain=0.0f;
        if(remain>1.0f) remain=1.0f;
        float surge=0.105f + 0.145f*remain;
        if(target<surge) target=surge;
    }

    if(vehicleChanged){
        g_tachSmoothedFrac=engineOff?0.0f:target;
    } else {
        float delta=target-g_tachSmoothedFrac;
        float maxUp=dt*3.20f;
        float maxDown=dt*(engineOff?3.85f:2.45f);
        if(delta>maxUp) delta=maxUp;
        if(delta<-maxDown) delta=-maxDown;
        g_tachSmoothedFrac += delta;
        g_tachSmoothedFrac=ClampF(g_tachSmoothedFrac,engineOff?0.0f:0.0f,0.94f);
    }

    if(engineOff) g_tachStartSurgeUntilTick=0;
    g_tachPrevEngineOff=engineOff;
    if(now!=0) g_lastTachTick=now;
    g_lastTachSpeedMps=speedMps;
    float drawFrac=g_tachSmoothedFrac;
    if(!engineOff && (gear==0 || gear==-99 || speedMps<0.70f)) drawFrac=ClampF(drawFrac + TachIdleNeedleTremor(now),0.0f,0.94f);
    float angle=(-130.0f+drawFrac*260.0f)*0.01745329252f;
    DrawTex(dev,f,&g_needle,tachX,tachY,size,size,angle,texColor);
}


static int DashboardLayoutModeIndex(int mode){
    if(mode==1 || mode==2 || mode==3 || mode==4) return mode;
    return 1;
}
static const char* DashboardLayoutModeName(int mode){
    if(mode==1) return "road";
    if(mode==2) return "marine";
    if(mode==3) return "aircraft";
    if(mode==4) return "bike";
    return "unknown";
}
static const char* DashboardLayoutPartName(int part){
    if(part==DASH_LAYOUT_PART_SPEED) return "speedometer";
    if(part==DASH_LAYOUT_PART_TACH) return "tachometer";
    if(part==DASH_LAYOUT_PART_FUEL) return "fuel";
    return "all";
}
static int DashboardLayoutPartValidForMode(int mode, int part){
    if(part==DASH_LAYOUT_PART_ALL) return 1;
    if(mode==1) return part==DASH_LAYOUT_PART_SPEED || part==DASH_LAYOUT_PART_TACH || part==DASH_LAYOUT_PART_FUEL;
    if(mode==4) return part==DASH_LAYOUT_PART_TACH || part==DASH_LAYOUT_PART_FUEL;
    if(mode==2 || mode==3) return part==DASH_LAYOUT_PART_SPEED || part==DASH_LAYOUT_PART_FUEL;
    return 0;
}
static int DashboardLayoutNextPartForMode(int mode, int part){
    int p=part;
    for(int i=0;i<DASH_LAYOUT_PART_MAX+1;i++){
        p++;
        if(p>=DASH_LAYOUT_PART_MAX) p=0;
        if(DashboardLayoutPartValidForMode(mode,p)) return p;
    }
    return 0;
}
static float* DashboardLayoutPartOffsetXRef(int mode, int part){
    DashboardLayoutEnsureDefaults();
    int mi=DashboardLayoutModeIndex(mode); if(part<0 || part>=DASH_LAYOUT_PART_MAX) part=0; return &g_layoutPartOffsetX[mi][part];
}
static float* DashboardLayoutPartOffsetYRef(int mode, int part){
    DashboardLayoutEnsureDefaults();
    int mi=DashboardLayoutModeIndex(mode); if(part<0 || part>=DASH_LAYOUT_PART_MAX) part=0; return &g_layoutPartOffsetY[mi][part];
}
static float* DashboardLayoutPartScaleRef(int mode, int part){
    DashboardLayoutEnsureDefaults();
    int mi=DashboardLayoutModeIndex(mode); if(part<0 || part>=DASH_LAYOUT_PART_MAX) part=0;
    if(g_layoutPartScale[mi][part]<0.05f) g_layoutPartScale[mi][part]=1.0f;
    return &g_layoutPartScale[mi][part];
}
static float DashboardLayoutPartOffsetXPx(int mode, int part, float W){ return (*DashboardLayoutPartOffsetXRef(mode,part))*W; }
static float DashboardLayoutPartOffsetYPx(int mode, int part, float H){ return (*DashboardLayoutPartOffsetYRef(mode,part))*H; }
static float DashboardLayoutPartScale(int mode, int part){ float v=*DashboardLayoutPartScaleRef(mode,part); if(v<0.05f) return 1.0f; return v; }
static int DashboardLayoutSelectedPart(int mode){
    int mi=DashboardLayoutModeIndex(mode);
    int p=g_layoutSelectedPart[mi];
    if(!DashboardLayoutPartValidForMode(mode,p)){ p=0; g_layoutSelectedPart[mi]=0; }
    return p;
}
static float DashboardLayoutEffectiveOffsetXPx(int mode, int part, float W){ return DashboardLayoutPartOffsetXPx(mode,DASH_LAYOUT_PART_ALL,W)+DashboardLayoutPartOffsetXPx(mode,part,W); }
static float DashboardLayoutEffectiveOffsetYPx(int mode, int part, float H){ return DashboardLayoutPartOffsetYPx(mode,DASH_LAYOUT_PART_ALL,H)+DashboardLayoutPartOffsetYPx(mode,part,H); }
static float DashboardLayoutEffectiveScale(int mode, int part){ return DashboardLayoutPartScale(mode,DASH_LAYOUT_PART_ALL)*DashboardLayoutPartScale(mode,part); }
static int DashboardLayoutKeyDown(int vk){ return (pGetAsyncKeyState && (pGetAsyncKeyState(vk)&0x8000)) ? 1 : 0; }
static void DashboardLayoutLog(const char* reason, int mode, int part, float W, float H){
    char b[960]; unsigned int p=0;
    float ox=*DashboardLayoutPartOffsetXRef(mode,part);
    float oy=*DashboardLayoutPartOffsetYRef(mode,part);
    float sc=DashboardLayoutPartScale(mode,part);
    float ex=DashboardLayoutEffectiveOffsetXPx(mode,part,W)/W;
    float ey=DashboardLayoutEffectiveOffsetYPx(mode,part,H)/H;
    float esc=DashboardLayoutEffectiveScale(mode,part);
    AppendStr(b,&p,"DASH_LAYOUT_TUNER reason=",960); AppendStr(b,&p,reason,960);
    AppendStr(b,&p," mode=",960); AppendStr(b,&p,DashboardLayoutModeName(mode),960);
    AppendStr(b,&p," part=",960); AppendStr(b,&p,DashboardLayoutPartName(part),960);
    AppendStr(b,&p," vehicleHandle=",960); AppendInt(b,&p,g_vehicleHandle,960);
    AppendStr(b,&p," screen=",960); AppendInt(b,&p,(int)W,960); AppendStr(b,&p,"x",960); AppendInt(b,&p,(int)H,960);
    AppendStr(b,&p," partOffsetXNorm=",960); AppendFloat2(b,&p,ox,960);
    AppendStr(b,&p," partOffsetYNorm=",960); AppendFloat2(b,&p,oy,960);
    AppendStr(b,&p," partOffsetXPx=",960); AppendFloat2(b,&p,ox*W,960);
    AppendStr(b,&p," partOffsetYPx=",960); AppendFloat2(b,&p,oy*H,960);
    AppendStr(b,&p," partScale=",960); AppendFloat2(b,&p,sc,960);
    AppendStr(b,&p," effectiveOffsetXNorm=",960); AppendFloat2(b,&p,ex,960);
    AppendStr(b,&p," effectiveOffsetYNorm=",960); AppendFloat2(b,&p,ey,960);
    AppendStr(b,&p," effectiveScale=",960); AppendFloat2(b,&p,esc,960);
    AppendStr(b,&p," iniHint=",960); AppendStr(b,&p,DashboardLayoutModeName(mode),960); AppendStr(b,&p,".",960); AppendStr(b,&p,DashboardLayoutPartName(part),960);
    AppendStr(b,&p,"OffsetX=",960); AppendFloat2(b,&p,ox,960);
    AppendStr(b,&p,";",960); AppendStr(b,&p,DashboardLayoutModeName(mode),960); AppendStr(b,&p,".",960); AppendStr(b,&p,DashboardLayoutPartName(part),960);
    AppendStr(b,&p,"OffsetY=",960); AppendFloat2(b,&p,oy,960);
    AppendStr(b,&p,";",960); AppendStr(b,&p,DashboardLayoutModeName(mode),960); AppendStr(b,&p,".",960); AppendStr(b,&p,DashboardLayoutPartName(part),960);
    AppendStr(b,&p,"Scale=",960); AppendFloat2(b,&p,sc,960);
    LogDashboardLine(b);
}
static void DashboardLayoutHandleInput(float W, float H){
    if(!pGetAsyncKeyState || W<100.0f || H<100.0f) return;

    int toggleDown=DashboardLayoutKeyDown(0x6F); // VK_DIVIDE
    if(toggleDown && !g_layoutPrevToggleDown){
        g_layoutTunerManualEnabled = g_layoutTunerManualEnabled ? 0 : 1;
        g_layoutTunerEnabled = (g_layoutTunerIniEnabled || g_layoutTunerManualEnabled) ? 1 : 0;
        g_layoutHelpLogged=0;
        g_layoutPrevPartDown=0;
        g_layoutPrevSaveDown=0;
        g_layoutPrevResetDown=0;
        LogDashboardLine(g_layoutTunerEnabled ? "DASH_LAYOUT_TUNER enabled=1 source=numpad_divide" : "DASH_LAYOUT_TUNER enabled=0 source=numpad_divide");
    }
    g_layoutPrevToggleDown=(short)toggleDown;
    if(!g_layoutTunerEnabled) return;

    int mode=g_mode;
    if(mode!=1 && mode!=2 && mode!=3 && mode!=4) return;
    DWORD now=pGetTickCount ? pGetTickCount() : 0u;
    if(!g_layoutHelpLogged){
        g_layoutHelpLogged=1;
        LogDashboardLine("DASH_LAYOUT_TUNER_KEYS numpad/ toggle; numpad* cycle element; numpad8/2/4/6 move; numpad7/9 scale; numpad5 reset selected; numpad0 save persistent layout to INI/log; hold Shift for larger steps");
    }
    int mi=DashboardLayoutModeIndex(mode);
    int part=DashboardLayoutSelectedPart(mode);
    int partDown=DashboardLayoutKeyDown(0x6A); // VK_MULTIPLY
    int saveDown=DashboardLayoutKeyDown(0x60); // VK_NUMPAD0
    int resetDown=DashboardLayoutKeyDown(0x65); // VK_NUMPAD5
    if(partDown && !g_layoutPrevPartDown){
        part=DashboardLayoutNextPartForMode(mode,part);
        g_layoutSelectedPart[mi]=part;
        DashboardLayoutLog("SELECT_PART",mode,part,W,H);
    }
    if(saveDown && !g_layoutPrevSaveDown){ g_layoutSaveRequested=1; DashboardLayoutLog("SAVE",mode,part,W,H); }
    if(resetDown && !g_layoutPrevResetDown){
        int mi=DashboardLayoutModeIndex(mode);
        *DashboardLayoutPartOffsetXRef(mode,part)=g_layoutDefaultOffsetX[mi][part];
        *DashboardLayoutPartOffsetYRef(mode,part)=g_layoutDefaultOffsetY[mi][part];
        *DashboardLayoutPartScaleRef(mode,part)=g_layoutDefaultScale[mi][part];
        DashboardLayoutLog("RESET",mode,part,W,H);
    }
    g_layoutPrevPartDown=(short)partDown;
    g_layoutPrevSaveDown=(short)saveDown;
    g_layoutPrevResetDown=(short)resetDown;
    if(now!=0 && g_layoutLastMoveTick!=0 && now-g_layoutLastMoveTick<18u) return;
    float stepPx=DashboardLayoutKeyDown(0x10) ? 10.0f : 2.0f; // VK_SHIFT
    float scaleStep=DashboardLayoutKeyDown(0x10) ? 0.015f : 0.003f;
    float dx=0.0f, dy=0.0f, ds=0.0f;
    if(DashboardLayoutKeyDown(0x64)) dx-=stepPx; // VK_NUMPAD4
    if(DashboardLayoutKeyDown(0x66)) dx+=stepPx; // VK_NUMPAD6
    if(DashboardLayoutKeyDown(0x68)) dy-=stepPx; // VK_NUMPAD8
    if(DashboardLayoutKeyDown(0x62)) dy+=stepPx; // VK_NUMPAD2
    if(DashboardLayoutKeyDown(0x67)) ds-=scaleStep; // VK_NUMPAD7
    if(DashboardLayoutKeyDown(0x69)) ds+=scaleStep; // VK_NUMPAD9
    if(dx==0.0f && dy==0.0f && ds==0.0f) return;
    float* ox=DashboardLayoutPartOffsetXRef(mode,part);
    float* oy=DashboardLayoutPartOffsetYRef(mode,part);
    float* sc=DashboardLayoutPartScaleRef(mode,part);
    *ox=ClampF(*ox + dx/W, -0.500f, 0.500f);
    *oy=ClampF(*oy + dy/H, -0.500f, 0.500f);
    *sc=ClampF(*sc + ds, 0.300f, 2.000f);
    g_layoutLastMoveTick=now;
    if(now!=0 && (g_layoutLastAdjustLogTick==0 || now-g_layoutLastAdjustLogTick>1200u)){
        g_layoutLastAdjustLogTick=now;
        DashboardLayoutLog("ADJUST",mode,part,W,H);
    }
}

static void RenderRoadDashboard(void* dev, DeviceFns* f, float W, float H, float opacity, u32 texColor){
    int layoutMode=(g_mode==4) ? 4 : 1;
    float allDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_ALL,W);
    float allDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_ALL,H);
    float allScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_ALL);
    float baseSpeedSize=SnapF(H*0.255f*allScale);
    float baseSpeedX=SnapF(W*0.835f+allDX);
    float baseSpeedY=SnapF(H*0.785f+allDY);
    DWORD now=pGetTickCount ? pGetTickCount() : 0u;
    int lowFuelActive=IsLowFuelIndicatorOn(now);
    u32 iconOffColor=AlphaMul(0x64FFFFFFu,opacity);
    u32 engineOffColor=AlphaMul(0x92FFFFFFu,opacity);
    u32 iconOnColor=texColor;

    if(g_mode==4){
        float tachScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_TACH);
        float tachDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_TACH,W);
        float tachDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_TACH,H);
        float tachSize=SnapF(baseSpeedSize*1.03f*tachScale);
        float tachX=SnapF(baseSpeedX-baseSpeedSize*0.665f+tachDX);
        float tachY=SnapF(baseSpeedY+baseSpeedSize*0.010f+tachDY);

        float fuelScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_FUEL);
        float fuelDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_FUEL,W);
        float fuelDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_FUEL,H);
        float fuelSize=SnapF(baseSpeedSize*0.78f*fuelScale);
        float fuelX=SnapF(baseSpeedX+fuelDX);
        float fuelY=SnapF(baseSpeedY+baseSpeedSize*0.060f+fuelDY);

        DrawTex(dev,f,&g_bikeTachometerBg,tachX,tachY,tachSize,tachSize,0.0f,texColor);

        /* Small elements use fixed shipped placement; only large elements are tunable. */
        float bikeWarningY=tachY+tachSize*0.024f;
        float lowX=tachX-tachSize*0.188f;
        float lowY=bikeWarningY;
        float lowSize=tachSize*0.132f;
        DrawTex(dev,f,&g_lowOff,lowX,lowY,lowSize,lowSize,0.0f,iconOffColor);
        if(lowFuelActive) DrawTex(dev,f,&g_lowOn,lowX,lowY,lowSize,lowSize,0.0f,iconOnColor);

        float engineX=tachX+tachSize*0.176f;
        float engineY=bikeWarningY;
        float engineSize=tachSize*0.138f;
        DrawTex(dev,f,&g_engineOffTex,engineX,engineY,engineSize,engineSize,0.0f,engineOffColor);
        if(g_checkEngineOn>0) DrawTex(dev,f,&g_engineOnTex,engineX,engineY,engineSize,engineSize,0.0f,iconOnColor);

        /* Warning icons belong to the dial face, below the moving needle. */
        DrawNeedleForTachApprox(dev,f,tachX,tachY,tachSize,g_speedMps,g_rpm,g_gear,texColor);

        float digitalX=tachX;
        float digitalY=tachY+tachSize*0.265f;
        int mph=RoundToInt(g_speedMps*2.23693629f);
        DrawNumberCentered(dev,f,mph,digitalX,digitalY,tachSize*0.120f,3,AlphaMul(0xFFE8E8E8u,opacity));

        DrawTex(dev,f,&g_bikeFuelBg,fuelX,fuelY,fuelSize,fuelSize,0.0f,texColor);
        DrawBikeFuelFillTex(dev,f,&g_bikeFuelFill,fuelX,fuelY,fuelSize,g_fuelPercent,opacity);

        float turnSize=fuelSize*0.155f*1.51f;
        float turnY=fuelY-fuelSize*0.255f+H*0.011111f;
        float leftX=fuelX-fuelSize*0.180f;
        float rightX=fuelX+fuelSize*0.180f;
        DrawTex(dev,f,&g_turnLeftOffTex,leftX,turnY,turnSize,turnSize,0.0f,iconOffColor);
        if(g_leftIndicatorOn>0) DrawTex(dev,f,&g_turnLeftOnTex,leftX,turnY,turnSize,turnSize,0.0f,iconOnColor);
        DrawTex(dev,f,&g_turnRightOffTex,rightX,turnY,turnSize,turnSize,0.0f,iconOffColor);
        if(g_rightIndicatorOn>0) DrawTex(dev,f,&g_turnRightOnTex,rightX,turnY,turnSize,turnSize,0.0f,iconOnColor);

        float gearX=fuelX;
        float gearY=fuelY+fuelSize*0.015f;
        DrawGearIndicator(dev,f,g_gear,gearX,gearY,fuelSize*0.165f*0.93f,AlphaMul(0xFFE8E8E8u,opacity));

        float beamX=fuelX;
        float beamY=fuelY+fuelSize*0.185f+H*0.009259f;
        float beamSize=fuelSize*0.145f*1.44f;
        DrawTex(dev,f,&g_highBeamOffTex,beamX,beamY,beamSize,beamSize,0.0f,iconOffColor);
        if(g_highBeamOn>0) DrawTex(dev,f,&g_highBeamOnTex,beamX,beamY,beamSize,beamSize,0.0f,iconOnColor);
        return;
    }

    float speedScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_SPEED);
    float speedDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_SPEED,W);
    float speedDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_SPEED,H);
    float speedSize=SnapF(baseSpeedSize*speedScale);
    float speedX=SnapF(baseSpeedX+speedDX);
    float speedY=SnapF(baseSpeedY+speedDY);

    float tachScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_TACH);
    float tachDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_TACH,W);
    float tachDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_TACH,H);
    float tachSize=SnapF(baseSpeedSize*0.820f*tachScale);
    float tachX=SnapF(baseSpeedX-baseSpeedSize*0.840f+tachDX);
    float tachY=SnapF(baseSpeedY+baseSpeedSize*0.025f+tachDY);

    DrawTex(dev,f,&g_tachometerBg,tachX,tachY,tachSize,tachSize,0.0f,texColor);

    float roadWarningY=tachY+tachSize*0.060f;
    float engineX=tachX+tachSize*0.188f;
    float engineY=roadWarningY;
    float engineSize=tachSize*0.145f;
    DrawTex(dev,f,&g_engineOffTex,engineX,engineY,engineSize,engineSize,0.0f,engineOffColor);
    if(g_checkEngineOn>0) DrawTex(dev,f,&g_engineOnTex,engineX,engineY,engineSize,engineSize,0.0f,iconOnColor);

    float lowX=tachX-tachSize*0.199f;
    float lowY=roadWarningY;
    float lowSize=tachSize*0.132f*1.10f;
    DrawTex(dev,f,&g_lowOff,lowX,lowY,lowSize,lowSize,0.0f,iconOffColor);
    if(lowFuelActive) DrawTex(dev,f,&g_lowOn,lowX,lowY,lowSize,lowSize,0.0f,iconOnColor);

    /* Tachometer face icons must remain below the moving needle. */
    DrawNeedleForTachApprox(dev,f,tachX,tachY,tachSize,g_speedMps,g_rpm,g_gear,texColor);

    float gearX=tachX+W*0.002083f;
    float gearY=tachY+tachSize*0.342f;
    DrawGearIndicator(dev,f,g_gear,gearX,gearY,tachSize*0.082f,AlphaMul(0xFFE8E8E8u,opacity));

    DrawTex(dev,f,&g_speedBg,speedX,speedY,speedSize,speedSize,0.0f,texColor);

    /* Road high beam is on the speedometer face: after the background, before its needle. */
    float beamX=speedX+speedSize*0.080f;
    float beamY=speedY+speedSize*0.090f;
    float beamSize=tachSize*0.138f*1.13f;
    DrawTex(dev,f,&g_highBeamOffTex,beamX,beamY,beamSize,beamSize,0.0f,iconOffColor);
    if(g_highBeamOn>0) DrawTex(dev,f,&g_highBeamOnTex,beamX,beamY,beamSize,beamSize,0.0f,iconOnColor);

    float turnSize=speedSize*0.122f*1.41f;
    float turnY=speedY+speedSize*0.195f+H*0.037037f;
    float leftX=speedX-speedSize*0.115f;
    float rightX=speedX+speedSize*0.115f;
    DrawTex(dev,f,&g_turnLeftOffTex,leftX,turnY,turnSize,turnSize,0.0f,iconOffColor);
    if(g_leftIndicatorOn>0) DrawTex(dev,f,&g_turnLeftOnTex,leftX,turnY,turnSize,turnSize,0.0f,iconOnColor);
    DrawTex(dev,f,&g_turnRightOffTex,rightX,turnY,turnSize,turnSize,0.0f,iconOffColor);
    if(g_rightIndicatorOn>0) DrawTex(dev,f,&g_turnRightOnTex,rightX,turnY,turnSize,turnSize,0.0f,iconOnColor);

    /* Speedometer face icons must remain below the moving needle. */
    DrawNeedleForSpeed(dev,f,speedX,speedY,speedSize,g_speedMps,160.0f,texColor);

    float fuelScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_FUEL);
    float fuelDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_FUEL,W);
    float fuelDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_FUEL,H);
    float fuelSize=SnapF(H*0.222f*allScale*fuelScale);
    /* Exact shared vertical centre line with the speedometer. */
    float fuelX=SnapF(speedX+fuelDX);
    float fuelY=SnapF(H*0.925f+allDY+fuelDY);
    DrawTex(dev,f,&g_fuelBg,fuelX,fuelY,fuelSize,fuelSize,0.0f,texColor);
    DrawFuelFillTex(dev,f,&g_fuelFill,fuelX,fuelY,fuelSize,g_fuelPercent,opacity);
}


static void RenderMarineDashboard(void* dev, DeviceFns* f, float W, float H, float opacity, u32 texColor){
    int layoutMode=2;
    float allDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_ALL,W);
    float allDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_ALL,H);
    float allScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_ALL);
    float baseSpeedSize=SnapF(H*0.255f*allScale);
    float baseSpeedX=SnapF(W*0.835f+allDX);
    float baseSpeedY=SnapF(H*0.785f+allDY);
    float speedScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_SPEED);
    float speedX=SnapF(baseSpeedX+DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_SPEED,W));
    float speedY=SnapF(baseSpeedY+DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_SPEED,H));
    float speedSize=SnapF(baseSpeedSize*speedScale);
    DrawTex(dev,f,&g_marineSpeedBg,speedX,speedY,speedSize,speedSize,0.0f,texColor);
    DrawTex(dev,f,&g_compassBg,speedX,speedY,speedSize,speedSize,-g_heading*0.01745329252f,texColor);
    DrawNeedleForSpeed(dev,f,speedX,speedY,speedSize,g_speedMps,90.0f,texColor);
    float fuelScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_FUEL);
    float fuelSize=SnapF(H*0.222f*allScale*fuelScale);
    float fuelX=SnapF(baseSpeedX+DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_FUEL,W));
    float fuelY=SnapF(H*0.925f+allDY+DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_FUEL,H));
    DrawTex(dev,f,&g_marineFuelBg,fuelX,fuelY,fuelSize,fuelSize,0.0f,texColor);
    DrawFuelFillTex(dev,f,&g_marineFuelFill,fuelX,fuelY,fuelSize,g_fuelPercent,opacity);
}


static void RenderAircraftDashboard(void* dev, DeviceFns* f, float W, float H, float opacity, u32 texColor){
    int layoutMode=3;
    float allDX=DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_ALL,W);
    float allDY=DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_ALL,H);
    float allScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_ALL);
    float baseSpeedSize=SnapF(H*0.255f*allScale);
    float baseSpeedX=SnapF(W*0.835f+allDX);
    float baseSpeedY=SnapF(H*0.785f+allDY);
    float speedScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_SPEED);
    float speedX=SnapF(baseSpeedX+DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_SPEED,W));
    float speedY=SnapF(baseSpeedY+DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_SPEED,H));
    float speedSize=SnapF(baseSpeedSize*speedScale);
    DrawTex(dev,f,&g_aircraftSpeedBg,speedX,speedY,speedSize,speedSize,0.0f,texColor);
    DrawNeedleForSpeed(dev,f,speedX,speedY,speedSize,g_speedMps,200.0f,texColor);
    float fuelScale=DashboardLayoutPartScale(layoutMode,DASH_LAYOUT_PART_FUEL);
    float panelW=SnapF(H*0.280f*allScale*fuelScale); float panelH=panelW;
    float panelX=SnapF(baseSpeedX+DashboardLayoutPartOffsetXPx(layoutMode,DASH_LAYOUT_PART_FUEL,W));
    float panelY=SnapF(H*0.925f+allDY+DashboardLayoutPartOffsetYPx(layoutMode,DASH_LAYOUT_PART_FUEL,H));
    DrawTex(dev,f,&g_aircraftFuelBg,panelX,panelY,panelW,panelH,0.0f,texColor);
    u32 numColor=AlphaMul(0xFFE8E8E8u,opacity);
    int altFt=RoundToInt(g_altitude*3.2808399f);
    if(altFt<0) altFt=0;
    if(altFt>99999) altFt=99999;
    DrawNumberRightAligned(dev,f,altFt,panelX+panelW*0.335f,panelY-panelH*0.095f,panelH*0.063f,5,numColor);
    if(g_fuelPercent>=0.0f){
        int pct=RoundToInt(ClampF(g_fuelPercent,0.0f,100.0f));
        DrawNumberRightAligned(dev,f,pct,panelX+panelW*0.335f,panelY+panelH*0.095f,panelH*0.063f,3,numColor);
    }
}


static DWORD g_dashboardCutsceneGuardUntil=0;
static int DashboardGuardActive(DWORD untilTick){
    DWORD now=pGetTickCount?pGetTickCount():0u;
    if(now==0 || untilTick==0) return 0;
    return now<untilTick;
}
static int DashboardCutsceneGuardActive(){ return DashboardGuardActive(g_dashboardCutsceneGuardUntil); }

struct PhoneAutoHideCandidate { u32 off; u8 value; };
static PhoneAutoHideCandidate g_phoneAutoHideCandidates[] = {
    {0x00D6BF8Bu,0xFFu},
    {0x00D6BF97u,0xFFu},
    {0x00D6BFC7u,0xFFu},
    {0x00D8E909u,0x01u}
};
static u32 g_phoneAutoHideBase=0;
static u32 g_phoneAutoHideSize=0;
static int g_phoneAutoHideStableState=0;
static int g_phoneAutoHideScore=0;
static int PhoneAutoHideEnsureBase(){
    if(g_phoneAutoHideBase && g_phoneAutoHideSize) return 1;
    void* base=GetMainModuleBase(); u32 size=0;
    if(!base || !GetMainSize(base,&size) || size==0) return 0;
    g_phoneAutoHideBase=(u32)base;
    g_phoneAutoHideSize=size;
    return 1;
}
static int ShouldSuppressDashboardByPhoneAutoHide(){
    if(!PhoneAutoHideEnsureBase()) return 0;
    int vote=0; int total=0;
    for(unsigned int i=0;i<sizeof(g_phoneAutoHideCandidates)/sizeof(g_phoneAutoHideCandidates[0]);i++){
        u32 off=g_phoneAutoHideCandidates[i].off;
        if(off>=g_phoneAutoHideSize) continue;
        total++;
        if(*((u8*)(g_phoneAutoHideBase+off))==g_phoneAutoHideCandidates[i].value) vote++;
    }

    if(total>=4){
        if(g_phoneAutoHideStableState){
            if(vote>=3) g_phoneAutoHideScore=10;
            else if(g_phoneAutoHideScore>0) g_phoneAutoHideScore-=2;
            if(g_phoneAutoHideScore<=0) g_phoneAutoHideStableState=0;
        }else{
            if(vote>=4) g_phoneAutoHideScore+=4;
            else if(vote>=3) g_phoneAutoHideScore+=1;
            else if(g_phoneAutoHideScore>0) g_phoneAutoHideScore-=2;
            if(g_phoneAutoHideScore<0) g_phoneAutoHideScore=0;
            if(g_phoneAutoHideScore>12) g_phoneAutoHideScore=12;
            if(g_phoneAutoHideScore>=8){ g_phoneAutoHideStableState=1; g_phoneAutoHideScore=10; }
        }
    }else{
        g_phoneAutoHideStableState=0;
        g_phoneAutoHideScore=0;
    }
    return g_phoneAutoHideStableState;
}

static void RenderDashboard(void* dev){
    if(g_mode!=1 && g_mode!=2 && g_mode!=3 && g_mode!=4) return;
    if(g_opacity<=0.02f) return;
    if(ShouldSuppressDashboardByStaleFrame()) return;
    if(DashboardCutsceneGuardActive()) return;
    if(ShouldSuppressDashboardByPhoneAutoHide()) return;
    if(!LoadTextures(dev)) return;
    DeviceFns f; GetFns(dev,&f);
    D3DVIEWPORT9_LVS vp; if(f.GetViewport(dev,&vp)<0 || vp.Width<100 || vp.Height<100) return;
    void* state=0;
    if(f.CreateStateBlock(dev,D3DSBT_ALL,&state)<0) state=0;
    if(state){ void** svt=*(void***)state; CaptureState_t Capture=(CaptureState_t)svt[4]; if(Capture) Capture(state); }
    PrepareDraw(dev,&f);
    float opacity=ClampF(g_opacity,0.0f,1.0f);
    u32 texColor=AlphaMul(0xFFFFFFFFu,opacity);
    float W=(float)vp.Width; float H=(float)vp.Height;
    DashboardLayoutHandleInput(W,H);
    if(g_mode==2) RenderMarineDashboard(dev,&f,W,H,opacity,texColor);
    else if(g_mode==3) RenderAircraftDashboard(dev,&f,W,H,opacity,texColor);
    else RenderRoadDashboard(dev,&f,W,H,opacity,texColor);
    f.SetTexture(dev,0,0);
    RestoreState(state);
    if(!g_firstFrameLogged){ g_firstFrameLogged=1; LogRaw("DASH_RENDERER_FIRST_FRAME_DRAWN"); }
}
static HRESULT __stdcall HookedEndScene(void* device){
    if(device) RenderDashboard(device);
    HRESULT r=0;
    if(g_originalEndScene){ EndScene_t orig=(EndScene_t)g_originalEndScene; r=orig(device); }
    return r;
}


extern "C" __declspec(dllexport) int LVSCE_DASH_Install(){ if(g_installed) return 1; g_installStarted=1; return InstallRenderer(); }
extern "C" __declspec(dllexport) int LVSCE_DASH_GetStatus(){ return g_status; }
extern "C" __declspec(dllexport) int LVSCE_DASH_GetBridgeVersion(){ return 106; }
extern "C" __declspec(dllexport) int LVSCE_DASH_SubmitFrame(int mode, int vehicleHandle, float speedMps, float fuelPercent, int lowFuel, float rpm, int gear, float heading, float altitude, int engineOn, int checkEngineOn, int lightsOn, int highBeamOn, int leftIndicatorOn, int rightIndicatorOn, float opacity){
    int oldVehicleHandle=g_vehicleHandle;
    g_mode=mode; g_vehicleHandle=vehicleHandle; g_speedMps=speedMps; g_fuelPercent=fuelPercent; g_lowFuel=lowFuel; g_rpm=rpm;
    if((mode!=1 && mode!=4) || vehicleHandle==0 || oldVehicleHandle!=vehicleHandle || g_submitLastVehicleHandleForGear!=vehicleHandle || g_gear==-99){
        g_gear=gear;
        g_submitLastVehicleHandleForGear=vehicleHandle;
    }
    g_heading=heading; g_altitude=altitude; g_engineOn=engineOn;
    if(checkEngineOn>=0) g_checkEngineOn=checkEngineOn;
    if(lightsOn>=0) g_lightsOn=lightsOn;
    if(highBeamOn>=0) g_highBeamOn=highBeamOn;
    if(leftIndicatorOn>=0) g_leftIndicatorOn=leftIndicatorOn;
    if(rightIndicatorOn>=0) g_rightIndicatorOn=rightIndicatorOn;
    g_opacity=opacity; if(pGetTickCount) g_lastSubmitTick=pGetTickCount(); return 1;
}
extern "C" __declspec(dllexport) int LVSCE_DASH_SubmitTelemetry(int vehicleAddress, int vehicleHandle, float speedMps, float signedSpeedMps, int gear, int mode){ SubmitTelemetryEx(vehicleAddress, vehicleHandle, speedMps, signedSpeedMps, gear, mode); return 1; }
extern "C" __declspec(dllexport) int LVSCE_DASH_SetRuntimeOptions(int cutsceneGuard, int layoutTunerEnabled){
    if(!ResolveImports()) return -1;
    DWORD now=pGetTickCount?pGetTickCount():0u;
    if(now!=0 && cutsceneGuard) g_dashboardCutsceneGuardUntil=now+1100u;
    int newIniState=layoutTunerEnabled ? 1 : 0;
    if(newIniState!=g_layoutTunerIniEnabled){
        g_layoutTunerIniEnabled=newIniState;
        g_layoutTunerEnabled=(g_layoutTunerIniEnabled || g_layoutTunerManualEnabled) ? 1 : 0;
        g_layoutHelpLogged=0;
        g_layoutPrevPartDown=0;
        g_layoutPrevSaveDown=0;
        g_layoutPrevResetDown=0;
        LogRaw(g_layoutTunerEnabled ? "DASH_LAYOUT_TUNER enabled=1 source=ini" : "DASH_LAYOUT_TUNER enabled=0 source=ini");
    }
    return 1;
}

extern "C" __declspec(dllexport) int LVSCE_DASH_SetLayout(int mode, int part, float offsetX, float offsetY, float scale){
    DashboardLayoutEnsureDefaults();
    if(!DashboardLayoutPartValidForMode(mode,part)) return 0;
    if(scale<0.45f || scale>2.50f) return 0;
    *DashboardLayoutPartOffsetXRef(mode,part)=offsetX;
    *DashboardLayoutPartOffsetYRef(mode,part)=offsetY;
    *DashboardLayoutPartScaleRef(mode,part)=scale;
    return 1;
}
extern "C" __declspec(dllexport) int LVSCE_DASH_GetLayout(int mode, int part, float* offsetX, float* offsetY, float* scale){
    DashboardLayoutEnsureDefaults();
    if(!DashboardLayoutPartValidForMode(mode,part) || !offsetX || !offsetY || !scale) return 0;
    *offsetX=*DashboardLayoutPartOffsetXRef(mode,part);
    *offsetY=*DashboardLayoutPartOffsetYRef(mode,part);
    *scale=*DashboardLayoutPartScaleRef(mode,part);
    return 1;
}
extern "C" __declspec(dllexport) int LVSCE_DASH_GetLayoutDefault(int mode, int part, float* offsetX, float* offsetY, float* scale){
    DashboardLayoutEnsureDefaults();
    if(!DashboardLayoutPartValidForMode(mode,part) || !offsetX || !offsetY || !scale) return 0;
    int mi=DashboardLayoutModeIndex(mode);
    *offsetX=g_layoutDefaultOffsetX[mi][part];
    *offsetY=g_layoutDefaultOffsetY[mi][part];
    *scale=g_layoutDefaultScale[mi][part];
    return 1;
}
extern "C" __declspec(dllexport) int LVSCE_DASH_ConsumeLayoutSaveRequest(){
    int result=g_layoutSaveRequested;
    g_layoutSaveRequested=0;
    return result;
}

extern "C" int __stdcall DllMain(void*, unsigned long reason, void*){ if(reason==DLL_PROCESS_ATTACH){} return 1; }
