const express = require('express');
const mongoose = require('mongoose');
const bodyParser = require('body-parser');

const fileRoutes = require('./routes/file');

const app = express();

mongoose.connect('mongodb+srv://RamcoTestUser:Password@cluster0-vqoq7.mongodb.net/DocumentManagement?&w=majority')
        .then(result => {
            app.listen(3000);
            console.log("Connected to Database");
        })
        .catch(err => console.log(err))

app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: false}));

app.use((req, res, next) => {
    res.setHeader('Access-Control-Allow-Origin', '*')
    res.setHeader('Access-Control-Allow-Headers', 'Origin, X-Requested-With, Content-Type, Accept, authorization')
    res.setHeader('Access-Control-Allow-Methods', 'GET, POST, PUT, DELETE, PATCH, OPTIONS')
    next();
})

app.use('/api/files', fileRoutes);