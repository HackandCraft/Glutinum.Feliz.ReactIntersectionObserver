#!/bin/bash

dotnet tool restore
dotnet fsi build.fsx -- $@
