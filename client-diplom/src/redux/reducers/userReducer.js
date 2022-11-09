import { LOGIN, SET_USER_DATA } from "../types"

const initState = {
    token:"",
    email:"",
    id:"",
    nickname:"",
    islogged: false
}

export const userReducer = (state = initState, action) => {
    switch(action.type){
        case LOGIN:{
            return { 
                ...state, 
                token: action.payload
            }
        }
        case SET_USER_DATA:{
            return {
                ...state,
                id: action.payload.id,
                nickname: action.payload.nickname,
                email: action.payload.email,
                islogged:true
            }
        }
        default:
            return state;
    }
}