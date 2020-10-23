const app = require('express')();
const mongoose = require('mongoose');
const bodyParser = require('body-parser');
const http = require('http').Server(app);
var io = require('socket.io')(http);

const User = require('./user');

mongoose.connect('mongodb+srv://RamcoTestUser:RamcoUser@cluster0-vqoq7.mongodb.net/NotificationComponent?&w=majority')
        .then(result => {
            http.listen(3000);
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

app.get('/', function(req, res) {
    res.sendFile(__dirname + '/index.html')
})

io.sockets.on('connection', function(socket){
    User.find()
    .then(result => {
        io.sockets.emit('subscribed', {users: result, firstTime: true})
    })
})

app.post("/api/users", function(req, res) {
    const user = new User({
        Name: req.body.name,
        PhoneNo: req.body.phone,
        Email: req.body.email
    })
    user.save().then(result => {
        res.status(201).json({
            message: "You are Subscribed!.. Thank you " + user.Name
        })
        User.find()
        .then(result => {
            io.sockets.emit('subscribed', {users: result, firstTime: false})
        })
    })
    .catch(err => {
        res.status(500).json({
            message: "Your Subsciption Failed.. due to" + err
        })
    })
})