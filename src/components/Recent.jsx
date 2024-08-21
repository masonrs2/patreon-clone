import { useState } from "react"; 
import { IoSearch } from "react-icons/io5";
import { categoryTags } from "../constants/constants";

 const Recent = () => {
  const [findCreatorsSearch, setFindCreatorsSearch] = useState("")

  const handleInputChange = (e) => {
    setFindCreatorsSearch(e.target.value)
  };

  return (
    <div>
        <div className="flex flex-col xl:justify-center mx-8 sm:mx-12 mt-64 gap-1 md:px-4 sm:px-6 lg:px-8 xl:px-32 2xl:px-64 justify-center">
          <div className="" >   
            <h1 className="text-4xl font-semibold">Find creators</h1>
            <div className=" w-full bg-zinc-800 rounded-full outline outline-stone-500 outline-1 px-3 py-[8px] text-sm text-stone-400 flex gap-3 mt-3" >
              <IoSearch className="text-stone-400" size={18} />
              <input
                type="text" 
                placeholder="Search creators or topics" 
                className="bg-transparent outline-none text-stone-400 w-full"
                value={findCreatorsSearch}
                onChange={handleInputChange}
              />
          </div>
        </div>
            <div className="flex flex-wrap gap-3 mt-3 w-full justify-center" >
                {/* For extra small screens (default) */}
                {categoryTags.slice(0, 4).map((tag, index) => (
                    <div key={index} className="sm:hidden bg-stone-700/60 p-[6px] px-[9px] rounded-md text-xs font-medium hover:cursor-pointer hover:bg-stone-700/70">
                        <p>{tag}</p>
                    </div>
                ))}
                {/* For small screens */}
                {categoryTags.slice(0, 5).map((tag, index) => (
                    <div key={index} className="hidden sm:block lg:hidden bg-stone-700/60 p-[6px] px-[9px] rounded-md text-xs font-medium hover:cursor-pointer hover:bg-stone-700/70">
                        <p>{tag}</p>
                    </div>
                ))}
                {/* For medium screens and above */}
                {categoryTags.map((tag, index) => (
                    <div key={index} className="hidden lg:block bg-stone-700/60 p-[6px] px-[9px] rounded-md text-xs font-medium hover:cursor-pointer hover:bg-stone-700/70">
                        <p>{tag}</p>
                    </div>
                ))}
            </div>
        </div>
    </div>
  )
}

export default Recent;