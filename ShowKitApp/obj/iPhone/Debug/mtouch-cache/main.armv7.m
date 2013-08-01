#include "monotouch/main.h"

extern void *mono_aot_module_ShowKitApp_info;
extern void *mono_aot_module_monotouch_info;
extern void *mono_aot_module_mscorlib_info;
extern void *mono_aot_module_System_info;
extern void *mono_aot_module_ParseTouch_info;
extern void *mono_aot_module_ShowKit_info;

void monotouch_register_modules ()
{
	mono_aot_register_module (mono_aot_module_ShowKitApp_info);
	mono_aot_register_module (mono_aot_module_monotouch_info);
	mono_aot_register_module (mono_aot_module_mscorlib_info);
	mono_aot_register_module (mono_aot_module_System_info);
	mono_aot_register_module (mono_aot_module_ParseTouch_info);
	mono_aot_register_module (mono_aot_module_ShowKit_info);

}

void monotouch_register_assemblies ()
{
	monotouch_open_and_register ("monotouch.dll");
	monotouch_open_and_register ("ParseTouch.dll");
	monotouch_open_and_register ("ShowKit.dll");

}

void monotouch_setup ()
{
	use_old_dynamic_registrar = TRUE;
	monotouch_create_classes ();
	monotouch_assembly_name = "ShowKitApp.exe";
	mono_use_llvm = FALSE;
	monotouch_log_level = 3;
	monotouch_debug_mode = TRUE;
	monotouch_new_refcount = FALSE;
	monotouch_sgen = FALSE;
}

