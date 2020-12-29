//install module firebase-admin

var admin = require('firebase-admin');
var serviceAccount = require('../../XamarinFCMServiceAccountKey.json');

admin.initializeApp({
  credential: admin.credential.cert(serviceAccount)
});

var messagingService = admin.messaging();

//É necessário definir uma condição, token ou topico
var message = {
    //é necessário definir topico de segmentação
    topic: 'android', 
    data: {
        Type: 'Sync_Request',
        Coverage: 'Full'
    },
    android: {
        priority:'high'
    },
    webpush: {
        headers: {
            Urgency: 'high'
        }
    }
};

messagingService.send(message)
    .then((response) => {
        console.log("Message successfully sended: ", response);
    })
    .catch((err) => {
        console.log("Error sending the message:\n",err);
    })
    .finally(() => process.exit());