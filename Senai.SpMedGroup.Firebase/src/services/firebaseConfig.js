    
import firebase from 'firebase';

const firebaseConfig = {
    apiKey: "AIzaSyDQI_JrqQulbqoxyCALVJtdHIKr_KHfv1A",
    authDomain: "spmedgroup-firebase.firebaseapp.com",
    databaseURL: "https://spmedgroup-firebase.firebaseio.com",
    projectId: "spmedgroup-firebase",
    storageBucket: "spmedgroup-firebase.appspot.com",
    messagingSenderId: "674060825149",
    appId: "1:674060825149:web:28aeca06655c815e"
  };

firebase.initializeApp(firebaseConfig);

export default firebase;