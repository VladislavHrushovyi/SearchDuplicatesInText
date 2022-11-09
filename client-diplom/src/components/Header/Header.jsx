import React from "react";
import "./Header.css"

import { Layout, Menu } from 'antd';
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import Text from "antd/lib/typography/Text";


export const Header = () => {

    const { Header } = Layout;
    const user = useSelector(r => r.user)
    console.log(user.islogged)
    const menuItems = [
        {
            name:"Головна",
            to: "main"
        }, 
        {
            name:"Звіт",
            to: "report"
        }, 
        {
            name: "Адмінка",
            to: "admin"
    }]

    return (
        <>
            <Header className="header">
                <div className="logo" />
                <Menu theme="dark" mode="horizontal">

                    {
                        menuItems.map((item, index) => {
                            return <Menu.Item key={index}>
                                        <Link to={item.to} >{item.name}</Link>
                                    </Menu.Item>
                        })
                    }
                    {
                        user.islogged ?
                        <Menu.Item className="user-name">
                            <Text  key={"qwerty"}>Вітаємо, {user.nickname}</Text>
                        </Menu.Item>
                        :
                        <Menu.Item className="login-btn">
                            <Link to={"login"} key={"qwerty"}>Увійти</Link>
                        </Menu.Item>
                    }
                </Menu>
            </Header>
        </>
    );
}