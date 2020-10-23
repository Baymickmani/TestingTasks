const File = require("../models/file");

exports.postFile = (req, res, next) => {
    const url = req.protocol + '://' + req.get("host");
    const tags = req.body.tags.split(",")
    const file = new File({
        fileUrl: url + "/uploads/" + req.file.filename,
        tags : tags
    });
    file.save().then(result => {
            res.status(201).json({
                message: "File Uploaded",
                id: result._id
            })
        })
        .catch(error => {
            res.status(500).json({
                message: "File Upload Failed"
            })
        })
}

exports.getFiles = (req, res, next) => {
    const tags = req.query.tags.split(",")
    File.find({tags: {$all: tags}})
        .then(result =>  {
            res.status(200).json({
                message: "Files found with specified tags are",
                files: result
            })
        })
        .catch(err => {
            res.status(500).json({
                message: "No Files Found"
            })    
        })
}