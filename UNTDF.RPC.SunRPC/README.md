# Setup en ambiente Windows

1. Descargar binario sunrcp desde https://gnuwin32.sourceforge.net/packages/sunrpc.htm
2. Descargar C/C++ devkit portatil desde https://github.com/skeeto/w64devkit
3. Agregar la hubicacion de los binarios de ambas descargas a la variable de entorno PATH
4. Definir division.idl
5. Usar el build command con cmd con permisos de administrador

## Build Command

rpcgen -aC -Y "C:/_DevTools/w64devkit/bin" division.idl 

# Setup en ambiente Debian
1. sudo apt update
2. sudo apt install build-essential git
3. Clonar repo
4. rpcgen -aCN <IDL file>
5. make -f <Makefile.filename>