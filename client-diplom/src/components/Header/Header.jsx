import React from "react";
import "./Header.css"

import { Layout, Menu } from 'antd';
import { Link } from "react-router-dom";


export const Header = () => {

    const { Header } = Layout;
    const menuItems = ["Головна", "Звіт", "Адмінка"]

    return (
        <>
            <Header className="header">
                <div className="logo" />
                <Menu theme="dark" mode="horizontal">

                    {
                        menuItems.map((item, index) => {
                            return <Menu.Item key={index}>
                                        <Link to="main" >{item}</Link>
                                    </Menu.Item>
                        })
                    }
                </Menu>
            </Header>
        </>
    );
}