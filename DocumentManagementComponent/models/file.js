var mongoose = require('mongoose'); 
  
var fileSchema = new mongoose.Schema({ 
    tags: [{type: String}],  
    fileUrl: String
}); 
   
module.exports = new mongoose.model('File', fileSchema);