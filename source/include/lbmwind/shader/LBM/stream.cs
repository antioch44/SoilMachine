#version 460 core

layout(local_size_x = 32, local_size_y = 1, local_size_z = 32) in;

#include lbm.cs

void main(){

  const uint ind = (gl_GlobalInvocationID.x*NY + gl_GlobalInvocationID.y)*NZ + gl_GlobalInvocationID.z;

  for(int q = 0; q < Q; q++){

    ivec3 n = ivec3(gl_GlobalInvocationID.xyz) + c[q];
    if(n.x < 0 || n.x >= NX) continue;
    if(n.y < 0 || n.y >= NY) continue;
    if(n.z < 0 || n.z >= NZ) continue;

    const int nind = (n.x*NY + n.y)*NZ + n.z;
    F[nind*Q+q] = FPROP[ind*Q+q];

  }




  // Optional: Driving Force (Wetnode Approach)

  if( gl_GlobalInvocationID.y == NY-1
  || gl_GlobalInvocationID.x == 0
  || gl_GlobalInvocationID.x == NX-1
  || gl_GlobalInvocationID.z == 0
  || gl_GlobalInvocationID.z == NZ-1
  ){

    for(int q = 0; q < Q; q++)
      F[ind*Q + q] = equilibrium(q, 1.0, force);

  }

  // CYCLONE

  /*

  if(gl_GlobalInvocationID.x == NX-1){
    for(int q = 0; q < Q; q++)
      F[ind*Q + q] = equilibrium(q, 1.0, vec3(0, 0, -0.1));
  }


  if(gl_GlobalInvocationID.x == 0){
    for(int q = 0; q < Q; q++)
      F[ind*Q + q] = equilibrium(q, 1.0, vec3(0, 0, 0.1));
  }


  if(gl_GlobalInvocationID.z == NZ-1){
    for(int q = 0; q < Q; q++)
      F[ind*Q + q] = equilibrium(q, 1.0, vec3(0.1, 0, 0));
  }


  if(gl_GlobalInvocationID.z == 0){
    for(int q = 0; q < Q; q++)
      F[ind*Q + q] = equilibrium(q, 1.0, vec3(-0.1, 0, 0));
  }

  */


}
