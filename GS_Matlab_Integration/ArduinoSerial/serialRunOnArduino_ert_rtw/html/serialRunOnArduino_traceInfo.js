function RTW_Sid2UrlHash() {
	this.urlHashMap = new Array();
	/* <Root>/Analog Input */
	this.urlHashMap["serialRunOnArduino:1"] = "serialRunOnArduino.c:40,46,88&serialRunOnArduino.h:54&serialRunOnArduino_data.c:26";
	/* <Root>/Gain */
	this.urlHashMap["serialRunOnArduino:7"] = "serialRunOnArduino.c:44&serialRunOnArduino.h:57&serialRunOnArduino_data.c:29";
	/* <Root>/Serial Transmit */
	this.urlHashMap["serialRunOnArduino:4"] = "serialRunOnArduino.c:43,45,91,107,114,115,121&serialRunOnArduino.h:47,48";
	this.getUrlHash = function(sid) { return this.urlHashMap[sid];}
}
RTW_Sid2UrlHash.instance = new RTW_Sid2UrlHash();
function RTW_rtwnameSIDMap() {
	this.rtwnameHashMap = new Array();
	this.sidHashMap = new Array();
	this.rtwnameHashMap["<Root>"] = {sid: "serialRunOnArduino"};
	this.sidHashMap["serialRunOnArduino"] = {rtwname: "<Root>"};
	this.rtwnameHashMap["<Root>/Analog Input"] = {sid: "serialRunOnArduino:1"};
	this.sidHashMap["serialRunOnArduino:1"] = {rtwname: "<Root>/Analog Input"};
	this.rtwnameHashMap["<Root>/Gain"] = {sid: "serialRunOnArduino:7"};
	this.sidHashMap["serialRunOnArduino:7"] = {rtwname: "<Root>/Gain"};
	this.rtwnameHashMap["<Root>/Serial Transmit"] = {sid: "serialRunOnArduino:4"};
	this.sidHashMap["serialRunOnArduino:4"] = {rtwname: "<Root>/Serial Transmit"};
	this.getSID = function(rtwname) { return this.rtwnameHashMap[rtwname];}
	this.getRtwname = function(sid) { return this.sidHashMap[sid];}
}
RTW_rtwnameSIDMap.instance = new RTW_rtwnameSIDMap();
