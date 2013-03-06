var assert = require("assert");
var routes = require("../routes");
var should = require("should");
describe('routes',function(){
	describe('device update',function(){
		it('should be a function',function(){
			routes.device.update.should.be.a.function
		});
		it('should save the posted device registration',function(){
			var returned;
			var expected={

			};
			var request = {
				body:{
					"Id":0,
					"Name":null,
					"Email":null,
					"Guid":"0F0F187A-9AD5-461A-BB56-810BFEF41553",
					"ServerId":0,
					"FacebookId":null
				}
			};
			var response = {
				send:function(obj){
					returned = obj;
				}
			};
			routes.device.update(request,response);
			returned.should.equal(expected);
		});
	});
});