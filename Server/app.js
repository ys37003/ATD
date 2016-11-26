var express = require("express");
var body_parser = require("body-parser");
var app = express();
var fs = require("fs");

var mongoose = require("mongoose");
var db = mongoose.connection;

app.use(body_parser.json());
app.use(body_parser.urlencoded({ extended: true }));

db.on("error", (err) => { console.log(err); });
db.once("open", () => { console.log("Coonected to db"); });

mongoose.connect("mongodb://localhost:27017/atd_db");

var scoreSchema = new mongoose.Schema({
    name: String,
    score: Number
});

var towerSchema = new mongoose.Schema({
    tower_type: Number,
    atk: Number,
    speed: Number,
    range: Number,
    hp: Number,
    tile_size: Number
});

var monsterSchema = new mongoose.Schema({
    monster_type: Number,
    hp: Number,
    atk: Number,
    atk_speed: Number,
    move_speed: Number,
    area: Number,
    drop_gold: Number
});

var shopSchema = new mongoose.Schema({
    tower_type: Number,
    tile_size: Number,
    cost: Number
});

var modelScore = mongoose.model("score", scoreSchema);
var modelTower = mongoose.model("tower", towerSchema);
var modelMonster = mongoose.model("monster", monsterSchema);
var modelShop = mongoose.model("shop", shopSchema);

app.get("/", (req, res) => {

    res.end("{ success: true }");
});

app.post("/rank", (req, res) => {

    var body = req.body;

    var newScore = new modelScore({
        name: body.name,
        score: body.score
    })

    newScore.save((err, score) => {
        console.log(score);
    });

    res.end();
});

app.post("/resources/:filename", (req, res) => {

    var fileName = req.params.filename;

    fs.readFile(fileName + ".json", "utf8", (err, data) => {

        if(err) {
            res.end(JSON.stringify({
                "success": false,
                "error": err
            }));

            return;
        }

        var data = JSON.parse(data)[fileName].data;

        data.forEach(function(element) {
            if(fileName == "towerstatus") {
                var model = new modelTower({
                    tower_type: element.tower_type,
                    atk: element.atk,
                    speed: element.speed,
                    range: element.range,
                    hp: element.hp,
                    tile_size: element.tile_size
                });

                model.save((err, model) => {
                    console.log(model);
                });
            }
            else if(fileName == "monsterStatus") {
                var model = new modelMonster({
                    monster_type: element.monster_type,
                    hp: element.hp,
                    atk: element.atk,
                    atk_speed: element.atk_speed,
                    move_speed:elementdata.move_speed,
                    area: element.area,
                    drop_gold: element.drop_gold
                });

                model.save((err, model) => {
                    console.log(model);
                });
            }
            else if(fileName == "shop") {
                var model = new modelShop({
                    tower_type: element.tower_type,
                    tile_size: element.tile_size,
                    cost: element.cost
                });

                model.save((err, model) => {
                    console.log(model);
                });
            }            
        }, this);

        console.log(data);

        res.end(JSON.stringify({
            "success": true,
            "data": data
        }));
    });
});

app.listen(8080, () => {

    console.log("server running");
});