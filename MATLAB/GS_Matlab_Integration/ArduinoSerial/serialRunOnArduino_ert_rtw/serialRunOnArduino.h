/*
 * Academic License - for use in teaching, academic research, and meeting
 * course requirements at degree granting institutions only.  Not for
 * government, commercial, or other organizational use.
 *
 * File: serialRunOnArduino.h
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

#ifndef RTW_HEADER_serialRunOnArduino_h_
#define RTW_HEADER_serialRunOnArduino_h_
#include <stddef.h>
#include <string.h>
#ifndef serialRunOnArduino_COMMON_INCLUDES_
# define serialRunOnArduino_COMMON_INCLUDES_
#include "rtwtypes.h"
#include "rtw_continuous.h"
#include "rtw_solver.h"
#include "MW_SerialWrite.h"
#include "arduino_analoginput_lct.h"
#endif                                 /* serialRunOnArduino_COMMON_INCLUDES_ */

#include "serialRunOnArduino_types.h"
#include "MW_target_hardware_resources.h"

/* Macros for accessing real-time model data structure */
#ifndef rtmGetErrorStatus
# define rtmGetErrorStatus(rtm)        ((rtm)->errorStatus)
#endif

#ifndef rtmSetErrorStatus
# define rtmSetErrorStatus(rtm, val)   ((rtm)->errorStatus = (val))
#endif

/* Block states (auto storage) for system '<Root>' */
typedef struct {
  codertarget_arduinobase_inter_T obj; /* '<Root>/Serial Transmit' */
  void *SerialTransmit_PWORK;          /* '<Root>/Serial Transmit' */
} DW_serialRunOnArduino_T;

/* Parameters (auto storage) */
struct P_serialRunOnArduino_T_ {
  uint32_T AnalogInput_p1;             /* Computed Parameter: AnalogInput_p1
                                        * Referenced by: '<Root>/Analog Input'
                                        */
  uint16_T Gain_Gain;                  /* Computed Parameter: Gain_Gain
                                        * Referenced by: '<Root>/Gain'
                                        */
};

/* Real-time Model Data Structure */
struct tag_RTM_serialRunOnArduino_T {
  const char_T *errorStatus;
};

/* Block parameters (auto storage) */
extern P_serialRunOnArduino_T serialRunOnArduino_P;

/* Block states (auto storage) */
extern DW_serialRunOnArduino_T serialRunOnArduino_DW;

/* Model entry point functions */
extern void serialRunOnArduino_initialize(void);
extern void serialRunOnArduino_step(void);
extern void serialRunOnArduino_terminate(void);

/* Real-time Model object */
extern RT_MODEL_serialRunOnArduino_T *const serialRunOnArduino_M;

/*-
 * The generated code includes comments that allow you to trace directly
 * back to the appropriate location in the model.  The basic format
 * is <system>/block_name, where system is the system number (uniquely
 * assigned by Simulink) and block_name is the name of the block.
 *
 * Use the MATLAB hilite_system command to trace the generated code back
 * to the model.  For example,
 *
 * hilite_system('<S3>')    - opens system 3
 * hilite_system('<S3>/Kp') - opens and selects block Kp which resides in S3
 *
 * Here is the system hierarchy for this model
 *
 * '<Root>' : 'serialRunOnArduino'
 */
#endif                                 /* RTW_HEADER_serialRunOnArduino_h_ */

/*
 * File trailer for generated code.
 *
 * [EOF]
 */
