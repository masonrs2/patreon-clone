import { sidebarIcons } from "../constants/constants"

const SideNavbar = () => {
  return (
    <div className="flex flex-col gap-6 p-3 items-center bg-stone-800 border-b border-b-stone-700/50 border-r border-r-stone-700/50 h-screen " >
      <div className=" flex flex-col gap-6 " >
        {
          sidebarIcons.map((icon, index) => {
            const IconComponent = icon.name   
            return (
            <div key={index} className="lg:flex gap-2 hover:bg-stone-500/20 p-3 rounded-md items-center hover:cursor-pointer" >
              <IconComponent size={icon.size} className="text-stone-100 focus:text-white" />
              <p className="hidden lg:block text-sm" >{icon.description}</p>
            </div>
          )})
        }
      </div>
        <p className="relative bottom-3 text-stone-600 hover:cursor-pointer">___</p>


        {/** Add user member subscription list here from DB once backend is created*/}
    </div>
  )
}

export default SideNavbar