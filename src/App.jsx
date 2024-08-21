// import './App.css'
import Home from './components/Home'
import SideNavbar from './components/SideNavbar'

function App() {  

  return (
    <div className="h-screen w-screen tracking-wide">
      <div className="flex">
        <SideNavbar />
        <Home />
      </div> 
    </div>
  )
}

export default App
