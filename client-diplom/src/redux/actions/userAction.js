import { LOGIN, SET_USER_DATA } from "../types"

export const Login = (data) => {
    return {
        type: LOGIN,
        payload: data
    }
}

export const SetUserInfo = (data) => {
    return {
        type: SET_USER_DATA,
        payload: data
    }
}