const express = require('express')

const extractFile = require("../middleware/file");
const FileController = require("../controllers/file");

const router = express.Router();

router.post('', extractFile, FileController.postFile);

router.get('', FileController.getFiles);

module.exports = router;