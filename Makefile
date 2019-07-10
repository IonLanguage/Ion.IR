all:
	git submodule init
	git submodule update
	cd Ion.Engine && make
	dotnet build
