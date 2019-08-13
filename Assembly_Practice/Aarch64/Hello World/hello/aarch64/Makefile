BINARIES=hello
AS=as
LD=ld

all:	${BINARIES}



lab4:        lab4a  lab4b

lab4a:		lab4a.s
		${AS}	-g	-o lab4a.o	lab4a.s
		${LD}		-o lab4a	lab4a.o


lab4b:		lab4b.s
		${AS}	-g	-o lab4b.o	lab4b.s
		${LD}		-o lab4b	lab4b.o



clean:	
		rm ${BINARIES} *.o || true



