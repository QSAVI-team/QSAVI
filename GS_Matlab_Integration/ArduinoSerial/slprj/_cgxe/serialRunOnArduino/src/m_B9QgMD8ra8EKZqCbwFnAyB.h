#ifndef __B9QgMD8ra8EKZqCbwFnAyB_h__
#define __B9QgMD8ra8EKZqCbwFnAyB_h__

/* Include files */
#include "simstruc.h"
#include "rtwtypes.h"
#include "multiword_types.h"
#include "slexec_vm_zc_functions.h"

/* Type Definitions */
#ifndef typedef_cell_wrap
#define typedef_cell_wrap

typedef struct {
  uint32_T f1[8];
} cell_wrap;

#endif                                 /*typedef_cell_wrap*/

#ifndef typedef_codertarget_arduinobase_internal_arduino_SerialWrite
#define typedef_codertarget_arduinobase_internal_arduino_SerialWrite

typedef struct {
  int32_T isInitialized;
  cell_wrap inputVarSize[1];
} codertarget_arduinobase_internal_arduino_SerialWrite;

#endif                                 /*typedef_codertarget_arduinobase_internal_arduino_SerialWrite*/

#ifndef typedef_struct_T
#define typedef_struct_T

typedef struct {
  real_T f1[2];
} struct_T;

#endif                                 /*typedef_struct_T*/

#ifndef typedef_b_struct_T
#define typedef_b_struct_T

typedef struct {
  char_T f1[7];
} b_struct_T;

#endif                                 /*typedef_b_struct_T*/

#ifndef typedef_c_struct_T
#define typedef_c_struct_T

typedef struct {
  char_T f1[4];
  char_T f2[6];
  char_T f3[6];
  char_T f4[6];
  char_T f5[8];
  char_T f6[7];
  char_T f7;
  real_T f8;
} c_struct_T;

#endif                                 /*typedef_c_struct_T*/

#ifndef typedef_d_struct_T
#define typedef_d_struct_T

typedef struct {
  char_T f1[5];
  char_T f2[4];
  char_T f3[6];
  char_T f4[5];
  char_T f5[6];
  char_T f6[5];
  char_T f7[6];
  char_T f8[6];
  char_T f9[7];
} d_struct_T;

#endif                                 /*typedef_d_struct_T*/

#ifndef typedef_e_struct_T
#define typedef_e_struct_T

typedef struct {
  char_T f1[6];
  char_T f2[6];
} e_struct_T;

#endif                                 /*typedef_e_struct_T*/

#ifndef typedef_InstanceStruct_B9QgMD8ra8EKZqCbwFnAyB
#define typedef_InstanceStruct_B9QgMD8ra8EKZqCbwFnAyB

typedef struct {
  SimStruct *S;
  codertarget_arduinobase_internal_arduino_SerialWrite sysobj;
  boolean_T sysobj_not_empty;
  void *emlrtRootTLSGlobal;
  uint8_T *u0;
} InstanceStruct_B9QgMD8ra8EKZqCbwFnAyB;

#endif                                 /*typedef_InstanceStruct_B9QgMD8ra8EKZqCbwFnAyB*/

/* Named Constants */

/* Variable Declarations */

/* Variable Definitions */

/* Function Declarations */

/* Function Definitions */
extern void method_dispatcher_B9QgMD8ra8EKZqCbwFnAyB(SimStruct *S, int_T method,
  void* data);

#endif
