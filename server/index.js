
// Install firebase-admin module.
var admin = require('firebase-admin');

// Firebase service credentials
// Can be found on Firebase -> Project Configurations -> Service Accounts
var serviceAccount = require('../../serviceAccountKey.json');

admin.initializeApp({
  credential: admin.credential.cert(serviceAccount)
});

var messagingService = admin.messaging();

// A message has to have an topic, condition or token to be sent.
var message = {
    topic: 'android', 

    // Custom key-value data.
    data: {
        Type: 'Sync_Request',
        Coverage: 'Full'
    },

    // Platform specific properties.
    android: {

        // Priority can be 'normal' or 'high'.
        // When 'normal', OS waits to next maintenance window to process the message.
        // When 'high', executes immediately.
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