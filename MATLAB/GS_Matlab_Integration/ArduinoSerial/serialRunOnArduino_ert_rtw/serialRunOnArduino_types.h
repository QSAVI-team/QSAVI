/*
 * Academic License - for use in teaching, academic research, and meeting
 * course requirements at degree granting institutions only.  Not for
 * government, commercial, or other organizational use.
 *
 * File: serialRunOnArduino_types.h
 *
 * Code generated for Simulink model 'serialRunOnArduino'.
 *
 * Model version                  : 1.46
 * Simulink Coder version         : 8.12 (R2017a) 16-Feb-2017
 * C/C++ source code generated on : Mon Oct 02 19:21:48 2017
 *
 * Target selection: ert.tlc
 * Embedded hardware selection: Atmel->AVR
 * Code generation objectives: Unspecified
 * Validation result: Not run
 */

#ifndef RTW_HEADER_serialRunOnArduino_types_h_
#define RTW_HEADER_serialRunOnArduino_types_h_
#include "rtwtypes.h"
#ifndef typedef_struct_T_serialRunOnArduino_T
#define typedef_struct_T_serialRunOnArduino_T

typedef struct {
  int16_T f1;
  int16_T f2;
} struct_T_serialRunOnArduino_T;

#endif                                 /*typedef_struct_T_serialRunOnArduino_T*/

#ifndef typedef_cell_wrap_serialRunOnArduino_T
#define typedef_cell_wrap_serialRunOnArduino_T

typedef struct {
  uint32_T f1[8];
} cell_wrap_serialRunOnArduino_T;

#endif                                 /*typedef_cell_wrap_serialRunOnArduino_T*/

#ifndef typedef_struct_T_serialRunOnArduino_i_T
#define typedef_struct_T_serialRunOnArduino_i_T

typedef struct {
  real_T f1[2];
} struct_T_serialRunOnArduino_i_T;

#endif                                 /*typedef_struct_T_serialRunOnArduino_i_T*/

#ifndef typedef_struct_T_serialRunOnArduin_ix_T
#define typedef_struct_T_serialRunOnArduin_ix_T

typedef struct {
  char_T f1[7];
} struct_T_serialRunOnArduin_ix_T;

#endif                                 /*typedef_struct_T_serialRunOnArduin_ix_T*/

#ifndef typedef_struct_T_serialRunOnArdui_ixy_T
#define typedef_struct_T_serialRunOnArdui_ixy_T

typedef struct {
  char_T f1[4];
  char_T f2[6];
  char_T f3[6];
  char_T f4[6];
  char_T f5[8];
  char_T f6[7];
  char_T f7;
  real_T f8;
} struct_T_serialRunOnArdui_ixy_T;

#endif                                 /*typedef_struct_T_serialRunOnArdui_ixy_T*/

#ifndef typedef_struct_T_serialRunOnArdu_ixyz_T
#define typedef_struct_T_serialRunOnArdu_ixyz_T

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
} struct_T_serialRunOnArdu_ixyz_T;

#endif                                 /*typedef_struct_T_serialRunOnArdu_ixyz_T*/

#ifndef typedef_struct_T_serialRunOnArd_ixyzh_T
#define typedef_struct_T_serialRunOnArd_ixyzh_T

typedef struct {
  char_T f1[6];
  char_T f2[6];
} struct_T_serialRunOnArd_ixyzh_T;

#endif                                 /*typedef_struct_T_serialRunOnArd_ixyzh_T*/

#ifndef typedef_codertarget_arduinobase_inter_T
#define typedef_codertarget_arduinobase_inter_T

typedef struct {
  int32_T isInitialized;
  cell_wrap_serialRunOnArduino_T inputVarSize;
  real_T port;
  real_T dataSizeInBytes;
  real_T dataType;
  real_T sendModeEnum;
  real_T sendFormatEnum;
} codertarget_arduinobase_inter_T;

#endif                                 /*typedef_codertarget_arduinobase_inter_T*/

/* Parameters (auto storage) */
typedef struct P_serialRunOnArduino_T_ P_serialRunOnArduino_T;

/* Forward declaration for rtModel */
typedef struct tag_RTM_serialRunOnArduino_T RT_MODEL_serialRunOnArduino_T;

#endif                                 /* RTW_HEADER_serialRunOnArduino_types_h_ */

/*
 * File trailer for generated code.
 *
 * [EOF]
 */
