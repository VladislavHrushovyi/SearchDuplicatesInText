import React from "react";
import "./Header.css"

import { Button, Layout, Menu } from 'antd';
import { Link } from "react-router-dom";
import { useSelector } from "react-redux";
import Text from "antd/lib/typography/Text";
import { SettingOutlined } from '@ant-design/icons';
import { useState } from "react";
import { SettingsModal } from "../SettingsModal/SettingsModal";


export const Header = () => {

    const { Header } = Layout;
    const user = useSelector(r => r.user)
    const [open, setOpen] = useState(false)
    const menuItems = [
        {
            name: "Головна",
            to: "main"
        },
        {
            name: "Звіт",
            to: "report"
        },
        // {
        //     name: "Адмінка",
        //     to: "admin"
        // }
    ]

    const showModal = () => {
        setOpen(true);
    };

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
                    <Menu.Item className="">
                        <Button type="primary" onClick={showModal} shape="circle" icon={<SettingOutlined />} />
                    </Menu.Item>
                    {
                        user.islogged ?
                            <>
                                <Menu.Item className="user-name">
                                    <Text key={"qwerty"}>Вітаємо, {user.nickname}</Text>
                                </Menu.Item>
                            </>
                            :
                            <Menu.Item className="login-btn">
                                <Link to={"login"} key={"qwerty"}>Увійти</Link>
                            </Menu.Item>
                    }
                </Menu>
                <SettingsModal open={open} setOpen={setOpen} />
            </Header>
        </>
    );
}