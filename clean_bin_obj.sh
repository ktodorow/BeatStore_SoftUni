#!/bin/bash

# This script removes all 'bin' and 'obj' folders recursively from the current directory.

# Check if the user is in the right directory
echo "You are about to delete all 'bin' and 'obj' folders in $(pwd)."
echo "Are you sure? (y/n)"
read -r confirmation

if [[ $confirmation != "y" ]]; then
  echo "Operation canceled."
  exit 1
fi

# Find and remove 'bin' and 'obj' directories
find . -type d \( -name "bin" -o -name "obj" \) -exec rm -rf {} +

# Print completion message
echo "All 'bin' and 'obj' folders have been removed successfully."
	
