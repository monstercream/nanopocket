"use strict";
// login_time의 경우. 
// get 값을 읽을 때 가공하기 위한 목적으로 사용한다. getter와 setter가 모두 사용가능하다.
module.exports = function(sequelize, DataTypes) {
  var user_core = sequelize.define('user_core', {
	  id : { type : DataTypes.INTEGER.UNSIGNED, primaryKey: true, autoIncrement: true}
	  , nick_name : { type : DataTypes.STRING(14) }
		, level : { type : DataTypes.INTEGER(3).UNSIGNED, defaultValue: 1}
		, exp : { type : DataTypes.INTEGER.UNSIGNED, defaultValue: 0}
	  , hearts : { type : DataTypes.INTEGER(3).UNSIGNED, defaultValue: 0}
		, cashs : { type : DataTypes.INTEGER(5).UNSIGNED, defaultValue: 0}
		, coins : { type : DataTypes.INTEGER(5).UNSIGNED, defaultValue: 0}
	  , login_time : { type : DataTypes.DATE, defaultValue: '2002-06-05 00:00:00'
		  , get:function() {
					var convertTime = new Date(this.getDataValue('login_time'));
				 	return convertTime.getTime()/1000;}
				}
  }, {
	  timestamps: false,
	  tableName: 'user_core'
  });
  return user_core;
};