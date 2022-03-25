from files.workers.api import *
port = 6969
for key in db.keys():
 del db[key]
 
@app.exception(sanic.exceptions.NotFound)
async def ignore_404s(request, exception):
    return sanic.response.json("ok")


print('\033[30;1m')
app.run(host="0.0.0.0", port=port)