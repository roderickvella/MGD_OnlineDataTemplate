const express = require("express");
const Sequelize = require("sequelize");
const bcrypt = require("bcryptjs");
const jwt = require("jsonwebtoken");

const app = express();
app.use(express.json());
const port = 3000;

//------------Database connection------------


//------------Test the DB Connection------------


//------------Table Models------------



//------------drop and resync database------------




//------------Functions------------



//------------Middleware--------------





//------------Routes (called from unity)------------
app.get("/", (req, res) => {
    res.send("Hello World!");
});


//------------server running------------
app.listen(port, () => {
  console.log(`Example app listening on port ${port}!`);
});