import './App.css';
import 'antd/dist/antd.min.css';
import { Header } from './components/Header/Header';
import { Layout } from 'antd';
import { Content } from 'antd/lib/layout/layout';
import { Routes, Route } from "react-router-dom";
import { MainPage } from './pages/MainPage/MainPage';

function App() {
  return (
    <>
    <Layout className='layout'>
      <Header />
      <Content style={{ padding: '0 50px' }}>
        <Routes>
          <Route path='/' element={<MainPage />} />
          <Route path='main' element={<MainPage />}/>
        </Routes>
      </Content>
    </Layout>
    </>
  );
}

export default App;
