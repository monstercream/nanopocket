var models = require('../models');
var express = require('express');
var router = express.Router();

/* GET users listing. */
router.get('/', function(req, res, next) {
  res.send('respond with a resource');
});
/* POST 사용자 아이디를 등록할 때 사용한다 */
router.post('/add/:nickName', function(req, res) {
  // 닉네임 중복 확인.
  models.user_core.findOne({where:{nick_name:req.params.nickName}})
  .then(function(findUserCoreData) {
    if(findUserCoreData) {
      // err : 닉네임 중복되는 경우.
      res.send('exist');
      return;
    }
    
    // 닉네임 등록
    models.user_core
    .create({nick_name:req.params.nickName, cashs:20, coins:1000, hearts:5})
    .then(function(createUserCore) {
      // 완료 결과 전송.
      res.send('done0'+createUserCore.id);
    });
  });
});

module.exports = router;
