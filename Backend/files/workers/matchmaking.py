from files.workers.api import *
@app.route('fortnite/api/matchmaking/session/<sid>', methods=['POST', 'GET'])
async def test(request,sid: str):
    reqbody = request.body
    reqheader = request.headers
    paramz = request.args
    res = requests.get('https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/matchmaking/session/'+sid+'', headers=reqheader)
    resjson = json.loads(res.text)
    resheaders = res.headers
    return sanic.response.json(resjson, headers=resheaders)
@app.route('fortnite/api/game/v2/voice/<accid>/createLoginToken', methods=['POST', 'GET'])
async def test(request,accid: str):
    reqbody = request.body
    reqheader = request.headers
    paramz = request.args
    res = requests.get('https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/voice/'+accid+'/createLoginToken', headers=reqheader)
    resjson = json.loads(res.text)
    resheaders = res.headers
    return sanic.response.json(resjson, headers=resheaders)
@app.route('fortnite/api/matchmaking/session/<sid>/<met>', methods=['POST', 'GET'])
async def test(request,sid: str,met: str):
    reqbody = request.body
    reqheader = request.headers
    paramz = request.args
    res = requests.get('https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/matchmaking/session/'+sid+'', params=paramz, headers=reqheader)
    resheaders = res.headers
    return sanic.response.json(json.loads(res.text), headers=resheaders)
@app.route('fortnite/api/game/v2/matchmaking/account/<accid>/session/<sid>/', methods=['POST', 'GET'])
async def test(request,accid: str,sid: str):
    reqbody = request.body
    reqheader = request.headers
    paramz = request.args
    res = requests.get('https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/matchmaking/account/'+accid+'/session/'+sid+'', params=paramz, headers=reqheader)
    resheaders = res.headers
    return sanic.response.json(json.loads(res.text), headers=resheaders)
#http://fort-private-server-backend2.edik1045kriker.repl.co/fortnite/api/game/v2/matchmakingservice/ticket/player/f1375842565b4cfe8036794f3751e388?partyPlayerIds=f1375842565b4cfe8036794f3751e388&bucketId=13920814:0:EU:playlist_playgroundv2&player.platform=Windows&player.subregions=FR,DE,GB&player.option.crossplayOptOut=false&player.option.partyId=3464778507cc44f48b30cf3626e3222a&player.option.splitScreen=false&party.WIN=true&input.KBM=true&player.input=KBM&player.option.microphoneEnabled=true&player.option.uiLanguage=en 
@app.route('fortnite/api/game/v2/matchmakingservice/ticket/player/<sid>/?partyPlayerIds=f1375842565b4cfe8036794f3751e388&bucketId=13920814:0:EU:playlist_playgroundv2&player.platform=Windows&player.subregions=FR,DE,GB&player.option.crossplayOptOut=false&player.option.partyId=3464778507cc44f48b30cf3626e3222a&player.option.splitScreen=false&party.WIN=true&input.KBM=true&player.input=KBM&player.option.microphoneEnabled=true&player.option.uiLanguage=en', methods=['POST', 'GET'])
async def test(request,sid: str):
    reqbody = request.body
    reqheader = request.headers
    request.args['player.platform'] = 'IOS'
    paramz = request.args
    res = requests.get('https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/matchmakingservice/ticket/player/'+sid+'/?partyPlayerIds=f1375842565b4cfe8036794f3751e388&bucketId=13920814:0:EU:playlist_playground&player.platform=Windows&player.subregions=FR,DE,GB&player.option.crossplayOptOut=false&player.option.partyId=3464778507cc44f48b30cf3626e3222a&player.option.splitScreen=false&party.WIN=true&input.KBM=true&player.input=KBM&player.option.microphoneEnabled=true&player.option.uiLanguage=en', params=paramz, headers=reqheader)
    resjson = json.loads(res.text)
    resheaders = res.headers
    open('files/cache/bearer.json','w+').truncate(0)
    open('files/cache/bearer.json','w+').write('\n'+reqheader['authorization'])
    return sanic.response.text(res.text, headers=resheaders)
@app.route('fortnite/api/matchmaking/session/<sid>/heartbeat', methods=['POST', 'GET'])
async def test(request,sid: str):
    reqbody = request.body
    reqheader = request.headers
    paramz = request.args
    res = requests.get('https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/matchmaking/session/'+sid+'/heartbeat/''', params=paramz, headers=reqheader)
    resheaders = res.headers
    return sanic.response.json(json.loads(res.text), headers=resheaders)
@app.route('fortnite/api/game/v2/matchmakingservice/ticket/player/<sid>/', methods=['POST', 'GET'])
async def test(request,sid: str):
    reqbody = request.body
    reqheader = request.headers
    request.args['player.platform'] = 'IOS'
    paramz = request.args
    res = requests.get('https://fortnite-public-service-prod11.ol.epicgames.com/fortnite/api/game/v2/matchmakingservice/ticket/player/'+sid+'', params=paramz, headers=reqheader)
    resjson = json.loads(res.text)
    resheaders = res.headers
    open('files/cache/bearer.json','w+').truncate(0)
    open('files/cache/bearer.json','w+').write('\n'+reqheader['authorization'])
    return sanic.response.text(res.text, headers=resheaders)