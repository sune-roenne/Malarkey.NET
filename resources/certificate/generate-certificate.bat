openssl req -x509 -nodes -days 730 -newkey rsa:2048 -keyout malarkey.key -out malarkey.crt -config openssl.conf -extensions v3_req
openssl pkcs12 -export -out malarkey.pfx -inkey malarkey.key -in malarkey.crt -password pass:%PSWD%