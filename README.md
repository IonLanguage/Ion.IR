![CircleCI](https://circleci.com/gh/IonLanguage/Ion.svg?style=svg)

#### Ion.IR

IonIR (Ion's intermediate representation) manipulation library. Used to create, manipulate and generate IonIR constructs.

#### Assembly

An Assembly is a term that refers to a file containing one or more routines.

### Constructs

#### Value

A Value can represent various number types, a character, or a string literal.

#### Type

A Type contains built-in metadata used to classify Values.

```llvm
create :i32 0 $zero;
```

#### Id

The Id construct serves as a delegate or reference to a Value.

#### Instruction

Instructions are operations with optional parameters bound to a Routine (see below).

```llvm
end void;
```

Instructions can have 0 and up to 2 arguments, separated by a space.

Instructions need to be bound to a Routine, and cannot stand on their own. If an instruction is not defined under a Routine, an error will be issued.

All instructions must end with the semi-colon character (";").

```llvm
call example;
```

#### Routine

A Routine is an isolated group of Instructions, identified by a name and accepting optional parameters.

```
@example(i32)
```

Everything below a routine (except other Routines) will be grouped and bound to it as Instructions.

Note that there is no concept of special Routines (ex. main or entry points).

### Examples

#### Echoing input argument

```llvm
@echoExample() :void
# Create the input value.
new :i32 $input;
set $input;

# Create the call's resulting value.
new :i32 $echo;

# Create the call's arguments.
new [args] $args;

# Append the input to the arguments.
bind $args $input;

# Call the remote routine with the arguments.
call routine2 $args;

# Terminate the local routine.
end $echo;

# Will return input argument (must be of type i32).
@echo(i32) :i32

# Access local arguments using $0 or $1 corresponding to their index.
end :i32 $0;
```

Without comments:

```llvm
@echoExample() :void
new :i32 $input;
set $input 0;
new :i32 $echo;
new [args] $args;
bind $args $input;
call echo $args;
end $echo;

@echo(i32) :i32
end :i32 $0;
```
