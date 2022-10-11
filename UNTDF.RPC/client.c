#include <stdio.h>

#include <rpc/rpc.h>    /* standard RPC include file */
#include "division.h"       /* this file is generated by rpcgen */

main(int argc, char* argv[])
{
    CLIENT* cl;         /* RPC handle */
    char* server;
    long* lresult;      /* return value */
    struct div_args divArgs;
    divArgs.dividendo = 12;
    divArgs.divisor = 3;

    if (argc != 2) {
        fprintf(stderr, "usage: %s hostname\n", argv[0]);
        exit(1);
    }


    if ((cl = clnt_create(server, DIV_PROG, TP4, "udp")) == NULL) {
        /*
         * can't establish connection with server
         */
        clnt_pcreateerror(server);
        exit(2);
    }


    if ((lresult = div_rpc_1(divArgs, cl)) == NULL) {
        clnt_perror(cl, server);
        exit(3);
    }
    printf("time on host %s = %ld\n", server, *lresult);


    clnt_destroy(cl);         /* done with the handle */
    exit(0);
}