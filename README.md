#### Ion.IR

IonIR (Ion's intermediate representation) manipulation library. Used to create, manipulate and generate IonIR constructs.

#### Assembly

An Assembly is a term that refers to a file containing one or more routines.

### Constructs

#### Value

A Value can represent various number types, a character, or a string literal.

#### Type

A Type contains built-in metadata used to classify Values.

```
create :i32 0
```

#### Id

The Id construct serves as a delegate or reference to a Value.

#### Instruction

Instructions are operations with optional parameters bound to a Routine (see below).

```
end void
```

Instructions can have 0 and up to 2 arguments, separated by a space.

Instructions need to be bound to a Routine, and cannot stand on their own. If an instruction is not defined under a Routine, an error will be issued.

```
call example
```

#### Routine

A Routine is an isolated group of Instructions, identified by a name.

```
@example
```

Everything below a routine (except other Routines) will be grouped and bound to it as Instructions.

Note that there is no concept of special Routines (ex. main or entry points).

### Examples

#### Echoing input argument

```
@echoExample
# Create the input value.
new [i32] $input
set $input 

# Create the call's resulting value.
new [i32] $echo

# Create the call's arguments.
new [args] $args

# Append the input to the arguments.
bind $args $input

# Call the remote routine with the arguments.
call routine2 $args

# Terminate the local routine.
end $echo

# Will return input argument (must be of type i32).
@echo

# Access local arguments using $0 or $1 corresponding on their index.
end [i32] $0
```

Without comments:

```
@echoExample
new [i32] $input
set $input 
new [i32] $echo
new [args] $args
bind $args $input
call routine2 $args
end $echo

@echo
end [i32] $0
```
