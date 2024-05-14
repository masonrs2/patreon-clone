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
        <div className="flex flex-col xl:justify-center mx-12 mt-64 ">
            <h1 className="text-4xl font-semibold">Find creators</h1>
            <div className=" w-full bg-zinc-800  rounded-full outline outline-stone-500 outline-1 px-3 py-[8px] text-sm text-stone-400 flex gap-3 mt-3" >
              <IoSearch className="text-stone-400" size={18} />
              <input
                type="text" 
                placeholder="Search creators or topics" 
                className="bg-transparent outline-none text-stone-400 w-full"
                value={findCreatorsSearch}
                onChange={handleInputChange}
              />
            </div>
            <div className="flex gap-2 mt-3 w-full justify-center" >
                {
                  categoryTags.map((tag, index) => (
                    <div key={index} className="bg-stone-300 p-[6px] px-[9px] rounded-md text-xs font-medium ">
                      <p>{tag}</p>
                    </div>
                  ))
                }
              </div>
        </div>
    </div>
  )
}

export default Recent