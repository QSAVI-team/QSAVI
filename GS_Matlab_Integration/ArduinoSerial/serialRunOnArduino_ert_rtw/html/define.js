function CodeDefine() { 
this.def = new Array();
this.def["IsrOverrun"] = {file: "ert_main_c.html",line:23,type:"var"};
this.def["OverrunFlag"] = {file: "ert_main_c.html",line:24,type:"var"};
this.def["rt_OneStep"] = {file: "ert_main_c.html",line:25,type:"fcn"};
this.def["main"] = {file: "ert_main_c.html",line:52,type:"fcn"};
this.def["serialRunOnArduino_DW"] = {file: "serialRunOnArduino_c.html",line:25,type:"var"};
this.def["serialRunOnArduino_M_"] = {file: "serialRunOnArduino_c.html",line:28,type:"var"};
this.def["serialRunOnArduino_M"] = {file: "serialRunOnArduino_c.html",line:29,type:"var"};
this.def["serialRunOnArduino_step"] = {file: "serialRunOnArduino_c.html",line:33,type:"fcn"};
this.def["serialRunOnArduino_initialize"] = {file: "serialRunOnArduino_c.html",line:73,type:"fcn"};
this.def["serialRunOnArduino_terminate"] = {file: "serialRunOnArduino_c.html",line:112,type:"fcn"};
this.def["DW_serialRunOnArduino_T"] = {file: "serialRunOnArduino_h.html",line:49,type:"type"};
this.def["struct_T_serialRunOnArduino_T"] = {file: "serialRunOnArduino_types_h.html",line:29,type:"type"};
this.def["cell_wrap_serialRunOnArduino_T"] = {file: "serialRunOnArduino_types_h.html",line:38,type:"type"};
this.def["struct_T_serialRunOnArduino_i_T"] = {file: "serialRunOnArduino_types_h.html",line:47,type:"type"};
this.def["struct_T_serialRunOnArduin_ix_T"] = {file: "serialRunOnArduino_types_h.html",line:56,type:"type"};
this.def["struct_T_serialRunOnArdui_ixy_T"] = {file: "serialRunOnArduino_types_h.html",line:72,type:"type"};
this.def["struct_T_serialRunOnArdu_ixyz_T"] = {file: "serialRunOnArduino_types_h.html",line:89,type:"type"};
this.def["struct_T_serialRunOnArd_ixyzh_T"] = {file: "serialRunOnArduino_types_h.html",line:99,type:"type"};
this.def["codertarget_arduinobase_inter_T"] = {file: "serialRunOnArduino_types_h.html",line:114,type:"type"};
this.def["P_serialRunOnArduino_T"] = {file: "serialRunOnArduino_types_h.html",line:119,type:"type"};
this.def["RT_MODEL_serialRunOnArduino_T"] = {file: "serialRunOnArduino_types_h.html",line:122,type:"type"};
this.def["serialRunOnArduino_P"] = {file: "serialRunOnArduino_data_c.html",line:24,type:"var"};
this.def["int8_T"] = {file: "rtwtypes_h.html",line:51,type:"type"};
this.def["uint8_T"] = {file: "rtwtypes_h.html",line:52,type:"type"};
this.def["int16_T"] = {file: "rtwtypes_h.html",line:53,type:"type"};
this.def["uint16_T"] = {file: "rtwtypes_h.html",line:54,type:"type"};
this.def["int32_T"] = {file: "rtwtypes_h.html",line:55,type:"type"};
this.def["uint32_T"] = {file: "rtwtypes_h.html",line:56,type:"type"};
this.def["real32_T"] = {file: "rtwtypes_h.html",line:57,type:"type"};
this.def["real64_T"] = {file: "rtwtypes_h.html",line:58,type:"type"};
this.def["real_T"] = {file: "rtwtypes_h.html",line:64,type:"type"};
this.def["time_T"] = {file: "rtwtypes_h.html",line:65,type:"type"};
this.def["boolean_T"] = {file: "rtwtypes_h.html",line:66,type:"type"};
this.def["int_T"] = {file: "rtwtypes_h.html",line:67,type:"type"};
this.def["uint_T"] = {file: "rtwtypes_h.html",line:68,type:"type"};
this.def["ulong_T"] = {file: "rtwtypes_h.html",line:69,type:"type"};
this.def["char_T"] = {file: "rtwtypes_h.html",line:70,type:"type"};
this.def["uchar_T"] = {file: "rtwtypes_h.html",line:71,type:"type"};
this.def["byte_T"] = {file: "rtwtypes_h.html",line:72,type:"type"};
this.def["creal32_T"] = {file: "rtwtypes_h.html",line:82,type:"type"};
this.def["creal64_T"] = {file: "rtwtypes_h.html",line:87,type:"type"};
this.def["creal_T"] = {file: "rtwtypes_h.html",line:92,type:"type"};
this.def["cint8_T"] = {file: "rtwtypes_h.html",line:99,type:"type"};
this.def["cuint8_T"] = {file: "rtwtypes_h.html",line:106,type:"type"};
this.def["cint16_T"] = {file: "rtwtypes_h.html",line:113,type:"type"};
this.def["cuint16_T"] = {file: "rtwtypes_h.html",line:120,type:"type"};
this.def["cint32_T"] = {file: "rtwtypes_h.html",line:127,type:"type"};
this.def["cuint32_T"] = {file: "rtwtypes_h.html",line:134,type:"type"};
this.def["pointer_T"] = {file: "rtwtypes_h.html",line:152,type:"type"};
this.def["getLoopbackIP"] = {file: "MW_target_hardware_resources_h.html",line:10,type:"var"};
}
CodeDefine.instance = new CodeDefine();
var testHarnessInfo = {OwnerFileName: "", HarnessOwner: "", HarnessName: "", IsTestHarness: "0"};
var relPathToBuildDir = "../ert_main.c";
var fileSep = "\\";
var isPC = true;
function Html2SrcLink() {
	this.html2SrcPath = new Array;
	this.html2Root = new Array;
	this.html2SrcPath["ert_main_c.html"] = "../ert_main.c";
	this.html2Root["ert_main_c.html"] = "ert_main_c.html";
	this.html2SrcPath["serialRunOnArduino_c.html"] = "../serialRunOnArduino.c";
	this.html2Root["serialRunOnArduino_c.html"] = "serialRunOnArduino_c.html";
	this.html2SrcPath["serialRunOnArduino_h.html"] = "../serialRunOnArduino.h";
	this.html2Root["serialRunOnArduino_h.html"] = "serialRunOnArduino_h.html";
	this.html2SrcPath["serialRunOnArduino_private_h.html"] = "../serialRunOnArduino_private.h";
	this.html2Root["serialRunOnArduino_private_h.html"] = "serialRunOnArduino_private_h.html";
	this.html2SrcPath["serialRunOnArduino_types_h.html"] = "../serialRunOnArduino_types.h";
	this.html2Root["serialRunOnArduino_types_h.html"] = "serialRunOnArduino_types_h.html";
	this.html2SrcPath["serialRunOnArduino_data_c.html"] = "../serialRunOnArduino_data.c";
	this.html2Root["serialRunOnArduino_data_c.html"] = "serialRunOnArduino_data_c.html";
	this.html2SrcPath["rtwtypes_h.html"] = "../rtwtypes.h";
	this.html2Root["rtwtypes_h.html"] = "rtwtypes_h.html";
	this.html2SrcPath["rtmodel_h.html"] = "../rtmodel.h";
	this.html2Root["rtmodel_h.html"] = "rtmodel_h.html";
	this.html2SrcPath["MW_target_hardware_resources_h.html"] = "../MW_target_hardware_resources.h";
	this.html2Root["MW_target_hardware_resources_h.html"] = "MW_target_hardware_resources_h.html";
	this.getLink2Src = function (htmlFileName) {
		 if (this.html2SrcPath[htmlFileName])
			 return this.html2SrcPath[htmlFileName];
		 else
			 return null;
	}
	this.getLinkFromRoot = function (htmlFileName) {
		 if (this.html2Root[htmlFileName])
			 return this.html2Root[htmlFileName];
		 else
			 return null;
	}
}
Html2SrcLink.instance = new Html2SrcLink();
var fileList = [
"ert_main_c.html","serialRunOnArduino_c.html","serialRunOnArduino_h.html","serialRunOnArduino_private_h.html","serialRunOnArduino_types_h.html","serialRunOnArduino_data_c.html","rtwtypes_h.html","rtmodel_h.html","MW_target_hardware_resources_h.html"];
