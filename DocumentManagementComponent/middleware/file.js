const multer = require('multer');

const storage = multer.diskStorage({
    destination: (req, file, cb) => {
        cb(null, "./uploads");
    },
    filename: (req, file, cb) => {
        const name = file.originalname.toLowerCase().split(' ').join('-');
        const ext = file.mimetype.split("/")[1];
        cb(null, name + '-' + Date.now() + '.' + ext);
    }
})

module.exports = multer({storage: storage}).single("file")