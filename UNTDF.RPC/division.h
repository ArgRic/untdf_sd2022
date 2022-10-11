/*
 * Please do not edit this file.
 * It was generated using rpcgen.
 */

#ifndef _DIVISION_H_RPCGEN
#define _DIVISION_H_RPCGEN

#include <rpc/rpc.h>


#ifdef __cplusplus
extern "C" {
#endif


struct div_args {
	long dividendo;
	long divisor;
};
typedef struct div_args div_args;

#define DIV_PROG 0x31234567
#define TP4 1

#if defined(__STDC__) || defined(__cplusplus)
#define DIV_RPC 1
extern  long * div_rpc_1(div_args *, CLIENT *);
extern  long * div_rpc_1_svc(div_args *, struct svc_req *);
extern int div_prog_1_freeresult (SVCXPRT *, xdrproc_t, caddr_t);

#else /* K&R C */
#define DIV_RPC 1
extern  long * div_rpc_1();
extern  long * div_rpc_1_svc();
extern int div_prog_1_freeresult ();
#endif /* K&R C */

/* the xdr functions */

#if defined(__STDC__) || defined(__cplusplus)
extern  bool_t xdr_div_args (XDR *, div_args*);

#else /* K&R C */
extern bool_t xdr_div_args ();

#endif /* K&R C */

#ifdef __cplusplus
}
#endif

#endif /* !_DIVISION_H_RPCGEN */