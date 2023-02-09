import struct
import os
import win32file
import win32pipe

def main():
    # Connect to the named pipe
    pipe = win32file.CreateFile("\\\\.\\pipe\\mypipe",
                                 win32file.GENERIC_READ | win32file.GENERIC_WRITE,
                                 0, None, win32file.OPEN_EXISTING, 0, None)

    # Read the array from the pipe
    array = []
    for i in range(6):
        buffer = win32file.ReadFile(pipe, 4)[1]
        value = struct.unpack("f", buffer)[0]
        array.append(value)

    # Perform the calculation
    result = sum(array)

    # Write the result to the pipe
    buffer = struct.pack("i", result)
    win32file.WriteFile(pipe, buffer)

    # Close the pipe
    win32file.CloseHandle(pipe)

if __name__ == '__main__':
    main()
