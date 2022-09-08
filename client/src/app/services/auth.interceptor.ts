import {HttpHandler, HttpInterceptor, HttpRequest} from "@angular/common/http";
import { Injectable } from "@angular/core";
import { CustomerService } from "./customer.service";

//injecting service to this service 
@Injectable()
export class AuthInterceptor implements HttpInterceptor{
    constructor(private authService: CustomerService) {}

    //here we ad the token crated to the authorization header 
    intercept(req: HttpRequest<any>, next: HttpHandler) {
        console.log("~!~(*~!)&~*&^(&)~!&(*~&)*!~(^~*))~)(!**!&~)(&*!)(~&");
        console.log("interceptor was called!");
        //const authToken = this.authService.getToken();
        let d_token_loc = localStorage.getItem("customer");
        console.log(d_token_loc);
        let d_token = "";
        //let token = d_token_loc['Access-Token'];
        JSON.parse(d_token_loc!,(key,value)=>{
            if(key == 'Access-Token'){
                d_token = "Bearer " + value;
            }
        });
        console.log(d_token);
        //console.log(typeof(d_token));
        //console.log(typeof(d_token_loc));
        if(d_token.length > 0){
            const authRequest = req.clone({
                withCredentials: true,
                headers: req.headers.set("Authorization", d_token!).set("Access-Control-Allow-Origin","http://localhost:4200")
                //headers: req.headers.set("Access-Control-Allow-Origin","http://localhost:4200"),
            });
            console.log(authRequest);
            return next.handle(authRequest);
        }
        else{
            console.log("send  req as is");
            console.log(req.headers);
            const authRequest = req.clone({
                headers: req.headers.set("Access-Control-Allow-Origin","http://localhost:4200")
            })
            return next.handle(authRequest);
        }
        //return next.handle(req);
    }
}