
var express = require('express');
var app = express();
var mysql = require('mysql');
var util = require('util');
var connection = mysql.createConnection({
    host: 'localhost',
    user: 'ab12yz89',
    password: 'ab12yz89',
    database: 'game1'

});

connection.connect(function (err) {

    if (err) {
        console.log('Error Connecting', err.stack);
        return;
    }
    console.log('Connected as id', connection.threadId);
});

app.get('/user/add', function (req, res) {
    var name = req.query.name;
    var password = req.query.password;
    var score = req.query.score;




    var ID = [[name, password,score]];
    AddUserID(ID, function (err, result) {
        res.end(result);
    });

});
app.get('/user/id', function (req, res) {


	ShowUserID(function (err, result) {
		res.end(result);
	});
});

app.get('/user/login/:name', function (req, res) {

	var login = req.params.name;

	LoginUserID(login, function (err, resualt) {
		res.end(resualt);
	});
});



var server = app.listen(8081, function () {
    console.log('Server Running');

});


function AddUserID(ID, callback) {
    var sql = 'INSERT INTO user(name,password,score) values ?';


    connection.query(sql, [ID], function (err) {
		var res = '[{"success" : "true"}]';

        if (err) {

            res = '{["success" : "false"]}';
            throw err;
        }

        callback(null, res);

    });
}
function ShowUserID(callback) {
	var json = "";
		var sql = 'SELECT name,score FROM user ORDER BY score DESC limit 10';

        connection.query(sql, function (err,rows,fields) {
            if (err) throw err;

        json = JSON.stringify(rows);

        callback(null,json);
    });
}
    function LoginUserID(name,callback) {
        var json = '';
		var sql = 'SELECT name,password FROM user WHERE name = ?';
        connection.query(sql,[name], function (err, rows, fields) {
            if (err) throw err;
    
            json = JSON.stringify(rows);
    
            callback(null,json);
        });
	}

app.get('/user/update', function (req, res) {
	var name = req.query.name;
	var score = req.query.score;
	UpdateScore(name, score, function (er, result) {
		res.end(result);
	});
});


function UpdateScore(name, score, callback) {
	var json = '';
	var sql = util.format('UPDATE user SET score = %d WHERE name = "%s"', score, name);

	connection.query(sql,function (err) {

		var result = '[{"success":"true"}]';

			if (err) {
				result = '[{"success":"false"}]';
				throw err;

			}

			callback(null, result);
		});
}

app.get('/user/loadscore/:name', function (req, res) {

	var login = req.params.name;

	LoadScore(login, function (err, result) {
		res.end(result);
	});
});


function LoadScore(name, callback) {
	var json = '';
	var sql = ('SELECT name,score FROM user WHERE name = ?');

	connection.query(sql, [name], function (err, rows, fields) {
		if (err) throw err;

		json = JSON.stringify(rows);

		callback(null, json);
	});
}