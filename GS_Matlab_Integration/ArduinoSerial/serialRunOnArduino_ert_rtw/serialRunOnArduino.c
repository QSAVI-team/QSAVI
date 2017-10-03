/*
 * Academic License - for use in teaching, academic research, and meeting
 * course requirements at degree granting institutions only.  Not for
 * government, commercial, or other organizational use.
 *
 * File: serialRunOnArduino.c
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

#include "serialRunOnArduino.h"
#include "serialRunOnArduino_private.h"
#define serialRunOnArdui_floatprecision (2.0)

/* Block states (auto storage) */
DW_serialRunOnArduino_T serialRunOnArduino_DW;

/* Real-time model */
RT_MODEL_serialRunOnArduino_T serialRunOnArduino_M_;
RT_MODEL_serialRunOnArduino_T *const serialRunOnArduino_M =
  &serialRunOnArduino_M_;

/* Model step function */
void serialRunOnArduino_step(void)
{
  uint16_T rtb_AnalogInput_0;
  int16_T k;
  uint8_T dataIn;
  boolean_T exitg1;

  /* S-Function (arduinoanaloginput_sfcn): '<Root>/Analog Input' */
  rtb_AnalogInput_0 = MW_analogRead(serialRunOnArduino_P.AnalogInput_p1);

  /* Start for MATLABSystem: '<Root>/Serial Transmit' incorporates:
   *  Gain: '<Root>/Gain'
   *  MATLABSystem: '<Root>/Serial Transmit'
   *  S-Function (arduinoanaloginput_sfcn): '<Root>/Analog Input'
   */
  k = 0;
  exitg1 = false;
  while ((!exitg1) && (k < 8)) {
    if (serialRunOnArduino_DW.obj.inputVarSize.f1[k] != 1UL) {
      for (k = 0; k < 8; k++) {
        serialRunOnArduino_DW.obj.inputVarSize.f1[k] = 1UL;
      }

      exitg1 = true;
    } else {
      k++;
    }
  }

  dataIn = (uint8_T)((uint32_T)serialRunOnArduino_P.Gain_Gain *
                     rtb_AnalogInput_0 >> 17);
  MW_Serial_write(serialRunOnArduino_DW.obj.port, &dataIn, 1.0,
                  serialRunOnArduino_DW.obj.dataSizeInBytes,
                  serialRunOnArduino_DW.obj.sendModeEnum,
                  serialRunOnArduino_DW.obj.dataType,
                  serialRunOnArduino_DW.obj.sendFormatEnum,
                  serialRunOnArdui_floatprecision, '\x00');
}

/* Model initialize function */
void serialRunOnArduino_initialize(void)
{
  /* Registration code */

  /* initialize error status */
  rtmSetErrorStatus(serialRunOnArduino_M, (NULL));

  /* states (dwork) */
  (void) memset((void *)&serialRunOnArduino_DW, 0,
                sizeof(DW_serialRunOnArduino_T));

  {
    cell_wrap_serialRunOnArduino_T varSizes;
    int16_T i;

    /* Start for S-Function (arduinoanaloginput_sfcn): '<Root>/Analog Input' */
    MW_pinModeAnalogInput(serialRunOnArduino_P.AnalogInput_p1);

    /* Start for MATLABSystem: '<Root>/Serial Transmit' */
    serialRunOnArduino_DW.obj.isInitialized = 0L;
    serialRunOnArduino_DW.obj.isInitialized = 1L;
    varSizes.f1[0] = 1UL;
    varSizes.f1[1] = 1UL;
    for (i = 0; i < 6; i++) {
      varSizes.f1[i + 2] = 1UL;
    }

    serialRunOnArduino_DW.obj.inputVarSize = varSizes;
    serialRunOnArduino_DW.obj.port = 0.0;
    serialRunOnArduino_DW.obj.dataSizeInBytes = 1.0;
    serialRunOnArduino_DW.obj.dataType = 0.0;
    serialRunOnArduino_DW.obj.sendModeEnum = 0.0;
    serialRunOnArduino_DW.obj.sendFormatEnum = 0.0;

    /* End of Start for MATLABSystem: '<Root>/Serial Transmit' */
  }
}

/* Model terminate function */
void serialRunOnArduino_terminate(void)
{
  /* Start for MATLABSystem: '<Root>/Serial Transmit' incorporates:
   *  Terminate for MATLABSystem: '<Root>/Serial Transmit'
   */
  if (serialRunOnArduino_DW.obj.isInitialized == 1L) {
    serialRunOnArduino_DW.obj.isInitialized = 2L;
  }

  /* End of Start for MATLABSystem: '<Root>/Serial Transmit' */
}

/*
 * File trailer for generated code.
 *
 * [EOF]
 */
