mergeInto(LibraryManager.library,
{

	UnlockButtonsExtern : function (value) {
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
				  console.log('Video ad open.');
				},
				onRewarded: () => {
				  console.log('Rewarded!');
				  SendMessage("GameManager", "UnlockRow", value);
				  myGameInstance.SendMessage("GameManager", "UnlockRow", value);
				},
				onClose: () => {
				  console.log('Video ad closed.');
				}, 
				onError: (e) => {
				  console.log('Error while open video ad:', e);
				}
			}
		})
	},

	ShowRewardedAdv : function () {
		ysdk.adv.showRewardedVideo({
			callbacks: {
				onOpen: () => {
				  console.log('Video ad open.');
				  SendMessage("MusicManager", "PauseMusic");
				  myGameInstance.SendMessage("MusicManager", "PauseMusic");
				},
				onRewarded: () => {
				  console.log('Rewarded!');
				},
				onClose: () => {
				  console.log('Video ad closed.');
					SendMessage("MusicManager", "UnPauseMusic");
				  myGameInstance.SendMessage("MusicManager", "UnPauseMusic");
				  SendMessage("GameManager", "GiveSecondLife");
				  myGameInstance.SendMessage("GameManager", "GiveSecondLife");
				}, 
				onError: (e) => {
				  console.log('Error while open video ad:', e);
				  SendMessage("MusicManager", "UnPauseMusic");
			  	myGameInstance.SendMessage("MusicManager", "UnPauseMusic");
				}
			}
		})
	},

	SaveExtern: function(date) {
    	var dateString = UTF8ToString(date);
    	var myobj = JSON.parse(dateString);
    	player.setData(myobj);
  	},

  	LoadExtern: function(){
    	player.getData().then(_date => {
        	const myJSON = JSON.stringify(_date);
        	myGameInstance.SendMessage('GameManager', 'SetPlayerInfo', myJSON);
        	SendMessage('GameManager', 'SetPlayerInfo', myJSON);
    	});
 	},

 	ShowAdv : function(){
        ysdk.adv.showFullscreenAdv({
          callbacks: {
          onClose: function(wasShown) {
            console.log("------------- closed --------------");
            // some action after close
            SendMessage("MusicManager", "UnPauseMusic", value);
				  	myGameInstance.SendMessage("MusicManager", "UnPauseMusic", value);
        	},
          onError: function(error) {
            // some action on error
            SendMessage("MusicManager", "UnPauseMusic", value);
				  	myGameInstance.SendMessage("MusicManager", "UnPauseMusic", value);
        	}
        }
        })
    },

	IsMobile: function()
	{
		return Module.SystemInfo.mobile;
	},

	GiveMePlayerData: function () {
    	myGameInstance.SendMessage('Yandex', 'SetName', player.getName());
    	myGameInstance.SendMessage('Yandex', 'SetPhoto', player.getPhoto("medium"));
  	},

	RateGame: function () {
    
    	ysdk.feedback.canReview()
        .then(({ value, reason }) => {
            if (value) {
                ysdk.feedback.requestReview()
                    .then(({ feedbackSent }) => {
                        console.log(feedbackSent);
                    })
            } else {
                console.log(reason)
            }
        })
  	},

	SetToLeaderboard : function(value) {
    	ysdk.getLeaderboards()
      	.then(lb => {
        lb.setLeaderboardScore('Score', value);
      });
  	},

  GetLang: function () {
    var lang = ysdk.environment.i18n.lang;
    var bufferSize = lengthBytesUTF8(lang) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(lang, buffer, bufferSize);
    return buffer;
    },

  AuthExtern: function () {
		auth();
	},

});


