from files.workers.api import *
from files.workers.profile import *
import shutil
#QueryProfile
@app.route('fortnite/api/game/v2/profile/<accountid>/client/<command>', methods=['POST'])
async def var(request,accountid: str,command: str):
  dirr = f'files/config/profiles/{accountid}'
  rvn = request.args['rvn'][0]
  #create profile if not exists
  if os.path.exists(dirr) == False:
    os.makedirs(dirr)
    shutil.copyfile('files/config/Default.json', dirr+'/settings.json')
  with open(dirr+'/settings.json') as f:
    account_settings = json.load(f)
  athena_json = json.loads('''{
	"profileRevision": 6888,
	"profileId": "athena",
	"profileChangesBaseRevision": 6888,
	"profileChanges": [{
		"changeType": "fullProfileUpdate",
		"profile": {
			"created": "2021-03-26T18:12:19.384Z",
			"updated": "2021-03-26T18:12:19.384Z",
			"rvn": 1,
			"wipeNumber": 1,
			"accountId": "accountid",
			"profileId": "athena",
			"version": "no_version",
			"items": {
				"loadout1": {
					"templateId": "CosmeticLocker:cosmeticlocker_athena",
					"attributes": {
						"locker_slots_data": {
							"slots": {
								"MusicPack": {
									"items": ["'''+account_settings["music"]+'''"]
								},
								"Character": {
									"items": ["'''+account_settings["character"]+'''"],
									"activeVariants": [null]
								},
								"Backpack": {
									"items": ["'''+account_settings["backpack"]+'''"],
									"activeVariants": [null]
								},
								"SkyDiveContrail": {
									"items": ["'''+account_settings["contrail"]+'''"],
									"activeVariants": [null]
								},
								"Dance": {
									"items": ["'''+account_settings["emote0"]+'''", "'''+account_settings["emote1"]+'''", "'''+account_settings["emote2"]+'''", "'''+account_settings["emote3"]+'''", "'''+account_settings["emote4"]+'''", "'''+account_settings["emote5"]+'''"]
								},
								"LoadingScreen": {
									"items": ["'''+account_settings["loadingscreen"]+'''"]
								},
								"Pickaxe": {
									"items": ["'''+account_settings["pickaxe"]+'''"],
									"activeVariants": [null]
								},
								"Glider": {
									"items": ["'''+account_settings["glider"]+'''"],
									"activeVariants": [null]
								},
								"ItemWrap": {
									"items": ["'''+account_settings["wrap0"]+'''", "'''+account_settings["wrap1"]+'''", "'''+account_settings["wrap2"]+'''", "'''+account_settings["wrap3"]+'''", "'''+account_settings["wrap4"]+'''", "'''+account_settings["wrap5"]+'''", "'''+account_settings["wrap6"]+'''"],
									"activeVariants": [null, null, null, null, null, null, null, null]
								}
							}
						},
						"use_count": 0,
						"banner_icon_template": "StandardBanner1",
						"banner_color_template": "DefaultColor1",
						"locker_name": "Locker",
						"item_seen": false,
						"favorite": false
					},
					"quantity": 1
				},
				"AthenaCharacter:pewdiepie": {
					"templateId": "AthenaCharacter:pewdiepie",
					"attributes": {
						"max_level_bonus": 0,
						"level": 999,
						"item_seen": 1,
						"xp": 999,
						"variants": [],
						"favorite": false
					},
					"quantity": 1
				},
'''+cosmaticsid+'''
				"AthenaBackpack:pewdiepieback": {
					"templateId": "AthenaBackpack:pewdiepieback",
					"attributes": {
						"max_level_bonus": 0,
						"level": 999,
						"item_seen": 1,
						"xp": 999,
						"variants": [],
						"favorite": false
					},
					"quantity": 1
				}
			},
			"stats": {
				"attributes": {
					"past_seasons": [],
					"season_match_boost": 999,
					"loadouts": ["loadout1"],
					"favorite_victorypose": "",
					"mfa_reward_claimed": false,
					"quest_manager": {
						"dailyLoginInterval": "2020-01-01T17:22:28.023Z",
						"dailyQuestRerolls": 1
					},
					"book_level": 100,
					"season_num": 16,
					"favorite_consumableemote": "",
					"banner_color": "DefaultColor1",
					"favorite_callingcard": "",
					"favorite_character": "",
					"favorite_spray": [],
					"book_xp": 100,
					"favorite_loadingscreen": "",
					"book_purchased": true,
					"lifetime_wins": 100,
					"favorite_hat": "",
					"level": '''+account_settings["level"]+''',
					"favorite_battlebus": "",
					"favorite_mapmarker": "",
					"favorite_vehicledeco": "",
					"accountLevel": '''+account_settings["level"]+''',
					"favorite_backpack": "",
					"favorite_dance": ["", "", "", "", "", ""],
					"inventory_limit_bonus": 0,
					"last_applied_loadout": "",
					"favorite_skydivecontrail": "",
					"favorite_pickaxe": "",
					"favorite_glider": "",
					"daily_rewards": {},
					"xp": '''+account_settings["level"]+''',
					"season_friend_match_boost": 999,
					"active_loadout_index": 0,
					"favorite_musicpack": "",
					"banner_icon": "StandardBanner1",
					"favorite_itemwraps": ["", "", "", "", "", "", ""]
				}
			},
			"commandRevision": 2618
		}
	}],
	"profileCommandRevision": 2618,
	"serverTime": "2021-03-26T18:12:19.384Z",
	"responseVersion": 1
}''')
  common_core_json = json.loads('{"profileRevision":1,"profileId":"common_core","profileChangesBaseRevision":1,"profileChanges":[{"changeType":"fullProfileUpdate","profile":{"_id":"accountid","created":"2021-03-24T19:19:01.988Z","updated":"2021-03-24T19:19:15.043Z","rvn":4,"wipeNumber":1,"accountId":"accountid","profileId":"common_core","version":"v2","items":{"Currency:MtxPurchased":{"attributes":{"platform":"EpicPC"},"quantity":'+account_settings["v-bucks"]+',"templateId":"Currency:MtxPurchased"}},"stats":{"attributes":{"mtx_affiliate":"gki","current_mtx_platform":"EpicPC","mtx_purchase_history":{}}},"commandRevision":2}}],"serverTime":"2021-03-26T18:12:19.384Z","profileCommandRevision":1,"responseVersion":1}')
  creative_json = json.loads('{"profileRevision":1,"profileId":"creative","profileChangesBaseRevision":1,"profileChanges":[{"changeType":"fullProfileUpdate","profile":{"created":"now","updated":"now","rvn":1,"wipeNumber":1,"accountId":"accountid","profileId":"creative","version":"fix_empty_users_again_july_2019","items":{},"stats":{"attributes":{"last_used_battlelab_file":"","max_island_plots":50,"publish_allowed":true,"support_code":"","last_used_plot":"","permissions":[],"creator_name":"","publish_banned":false,"inventory_limit_bonus":0}},"_id":"accountid"}}],"serverTime":"2021-03-26T18:12:19.384Z","profileCommandRevision":1,"responseVersion":1}')
 #profile commands
  if request.args['profileId'] == ['athena'] and command == 'QueryProfile':
   return sanic.response.json(athena_json)
   print('client '+accountid+' has logged in.')
  if request.args['profileId'] == ['common_core'] and command == 'QueryProfile':
   return sanic.response.json(common_core_json)
  if request.args['profileId'] == ['collections'] and command == 'QueryProfile':
   return sanic.response.json([])
  if request.args['profileId'] == ['common_public'] and command == 'QueryProfile':
   return sanic.response.json([])
  if request.args['profileId'] == ['creative'] and command == 'QueryProfile':
   return sanic.response.json(creative_json)   
  #SetMtxPlatform
  if request.args['profileId'] == ['common_core'] and command == 'SetMtxPlatform':
   return sanic.response.json(common_core_json)
  #
  if request.args['profileId'] == ['common_core'] and command == 'SetMtxPlatform':
   return sanic.response.json(common_core_json)
  if request.args['profileId'] == ['athena'] and command == 'SetHardcoreModifier':
   return sanic.response.json(athena_json)
  #ClientQuestLogin
  if request.args['profileId'] == ['athena'] and command == 'ClientQuestLogin':
   return sanic.response.json(json.loads('{"profileRevision":6888,"profileId":"athena","profileChangesBaseRevision":6888,"profileChanges":[],"serverTime":"2021-03-29T19:04:47.462Z","profileCommandRevision":2618,"responseVersion":1}'))
#SetCosmeticLockerSlot
  if command == 'SetCosmeticLockerSlot':
    content = json.loads(request.body)
     #character
    if content['category'] == 'Character':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['character'] = content['itemToSlot']
     account_settings["character"] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    #backpack
    if content['category'] == 'Backpack':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['backpack'] = content['itemToSlot']
     account_settings["backpack"] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    #pickaxe
    if content['category'] == 'Pickaxe':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['pickaxe'] = content['itemToSlot']
     account_settings["pickaxe"] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    #glider
    if content['category'] == 'Glider':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['glider'] = content['itemToSlot']
     account_settings["glider"] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    #SkyDiveContrail
    if content['category'] == 'SkyDiveContrail':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['contrail'] = content['itemToSlot']
     account_settings["contrail"] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    #loadingscreen
    if content['category'] == 'LoadingScreen':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['loadingscreen'] = content['itemToSlot']
     account_settings["loadingscreen"] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    #musicpack
    if content['category'] == 'MusicPack':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['music'] = content['itemToSlot']
     account_settings["music"] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
   #dance
    if content['category'] == 'Dance':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['emote'+str(content['slotIndex'])] = content['itemToSlot']
     account_settings['emote'+str(content['slotIndex'])] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    #ItemWrap
    if content['category'] == 'ItemWrap':
     with open (dirr+'/settings.json','r') as f:
       config = json.load(f)
     config['wrap'+str(content['slotIndex'])] = content['itemToSlot']
     account_settings['wrap'+str(content['slotIndex'])] = content['itemToSlot']
     with open (dirr+'/settings.json','w+') as f:
       json.dump(config, f)
    newathena = json.loads('''{
   "profileChanges" : [
      {
         
         "attributeName" : "locker_slots_data",
         "attributeValue" : {
            "slots" : {
               "Backpack" : {
                  "activeVariants" : [
                     {
                        "variants" : []
                     }
                  ],
                  "items" : ["'''+account_settings["backpack"]+'''"]
               },
               "Character" : {
					        "activeVariants": [{
						        "variants": []
				    	}],
                  "items" : ["'''+account_settings["character"]+'''"]
               },
               "Dance" : {
                  "items" : ["'''+account_settings["emote0"]+'''", "'''+account_settings["emote1"]+'''", "'''+account_settings["emote2"]+'''", "'''+account_settings["emote3"]+'''", "'''+account_settings["emote4"]+'''", "'''+account_settings["emote5"]+'''"]
               },
               "Glider" : {
                  "items" : ["'''+account_settings["glider"]+'''"]
               },
               "ItemWrap" : {
                  "activeVariants" : [ "", "", "", "", "", "", "" ],
                  "items" : ["'''+account_settings["wrap0"]+'''", "'''+account_settings["wrap1"]+'''", "'''+account_settings["wrap2"]+'''", "'''+account_settings["wrap3"]+'''", "'''+account_settings["wrap4"]+'''", "'''+account_settings["wrap5"]+'''", "'''+account_settings["wrap6"]+'''"]
               },
               "LoadingScreen" : {
                  "activeVariants" : [],
                  "items" : ["'''+account_settings["loadingscreen"]+'''"]
               },
               "MusicPack" : {
                  "activeVariants" : [],
                  "items" : ["'''+account_settings["music"]+'''"]
               },
               "Pickaxe" : {
                  "activeVariants" : [],
                  "items" : ["'''+account_settings["pickaxe"]+'''"]
               },
               "SkyDiveContrail" : {
                  "activeVariants" : [],
                  "items" : ["'''+account_settings["contrail"]+'''"]
               }
            }
         },
         "changeType" : "itemAttrChanged",
         "itemId" : "loadout1"
      }
   ],
   "profileChangesBaseRevision" : 6889,
   "profileCommandRevision" : 2619,
   "profileId" : "athena",
   "profileRevision" : 6889,
   "responseVersion" : 1,
   "serverTime" : "now"
}''')
  return sanic.response.json(newathena)