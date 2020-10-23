var mongoose = require('mongoose'); 
  
var userSchema = new mongoose.Schema({ 
    Name: {
        type: String,
        required: true
    },
    PhoneNo: {
        type: String,
        required: true
    },
    Email: {
        type: String,
        required: true
    }
}); 
   
module.exports = new mongoose.model('User', userSchema);