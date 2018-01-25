/* Include files */

#include "serialRunOnArduino_cgxe.h"
#include "m_B9QgMD8ra8EKZqCbwFnAyB.h"

unsigned int cgxe_serialRunOnArduino_method_dispatcher(SimStruct* S, int_T
  method, void* data)
{
  if (ssGetChecksum0(S) == 2357229112 &&
      ssGetChecksum1(S) == 3002877598 &&
      ssGetChecksum2(S) == 779637914 &&
      ssGetChecksum3(S) == 2986241858) {
    method_dispatcher_B9QgMD8ra8EKZqCbwFnAyB(S, method, data);
    return 1;
  }

  return 0;
}
