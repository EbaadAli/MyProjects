BINARIES=hello-nasm hello-gas

all:	${BINARIES}
AS_ARGS=-g

hello-nasm:	hello-nasm.s
		nasm 	-g 	-o hello-nasm.o	-f elf64 	hello-nasm.s
		ld 	 	-o hello-nasm	 		hello-nasm.o

hello-gas:	hello-gas.s
		as	${AS_ARGS}	-o hello-gas.o		hello-gas.s
		ld			-o hello-gas		hello-gas.o

clean:	
		rm ${BINARIES} *.o || true


lab4:           lab4b   lab4a


lab4a:          lab4a.s
	        as -g -o lab4a.o lab4a.s
		ld -g -o lab4a lab4a.o

lab4b:          lab4b.s
	        as -g -o lab4b.o lab4b.s
		ld -g -o lab4b lab4b.o

