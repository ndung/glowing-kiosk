####################################################################

MODULE = tkusb

CMDFILE = /tmp/mkmodule

FRIEND = mousedev

CC = gcc

INCLUDE = -I include

C_FLAGS = -Wall -O -pipe $(INCLUDE)

###################################################################

all: do_build_module

do_build_module: $(MODULE).o

%.o: %.c %.h %_dbg.h $(CMDFILE)
	tcl mkmodule.tcl $(CMDFILE) $(FRIEND) $@ $(INCLUDE)

$(CMDFILE): mkmodule.cmd
	cp -f $< $@

new: clean all

clean:
	rm -f $(MODULE).o $(CMDFILE) core *.bak


