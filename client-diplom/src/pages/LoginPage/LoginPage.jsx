import { Col, Row, Tabs } from "antd";
import { SolutionOutlined, IdcardOutlined } from '@ant-design/icons';
import React from "react";
import "./LoginPage.css"
import { LoginForm } from "../../components/LoginForm/LoginForm";
import { SignUpForm } from "../../components/SignUpForm/SignUpForm";

export const LoginPage = () => {

    return (
        <>
            <div className="site-layout-content">
                <Row justify="center">
                    <Col className="login-container">
                        <Tabs centered >
                            <Tabs.TabPane tab={<span><IdcardOutlined /> Авторизація</span>} key="item-1">
                                <LoginForm  className="login-container"/>
                            </Tabs.TabPane>
                            <Tabs.TabPane tab={<span><SolutionOutlined /> Реєстрація</span>} key="item-2">
                                <SignUpForm />
                            </Tabs.TabPane>
                        </Tabs>
                    </Col>
                </Row>
            </div>
        </>
    )
} 