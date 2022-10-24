import React from "react";
import "./Header.css"

import { Layout, Menu } from 'antd';
import { Link } from "react-router-dom";


export const Header = () => {

    const { Header } = Layout;
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
                </Menu>
            </Header>
        </>
    );
}